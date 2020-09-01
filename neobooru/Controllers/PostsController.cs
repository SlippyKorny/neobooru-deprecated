using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using neobooru.Models;
using neobooru.ViewModels;
using neobooru.ViewModels.Forms;

namespace neobooru.Controllers
{
    public class PostsController : Controller
    {
        private readonly NeobooruDataContext _db;

        private readonly UserManager<NeobooruUser> _userManager;

        private readonly SignInManager<NeobooruUser> _signInManager;

        private readonly string[] _subsectionPages = {"List", "Trending", "Upload", "Help"};

        public PostsController(NeobooruDataContext db, UserManager<NeobooruUser> userManager,
            SignInManager<NeobooruUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> List(string tagString, int page, string sortBy, string orderBy)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            List<ArtThumbnailViewModel> thumbnails;
            if (tagString == null && sortBy == null && orderBy == null)
            {
                thumbnails = new List<ArtThumbnailViewModel>();
                await _db.Arts.Include(a => a.Author)
                    .OrderByDescending(a => a.CreatedAt).Skip(page * 20).Take(20)
                    .ForEachAsync(a => thumbnails.Add(new ArtThumbnailViewModel(a)));

                ViewBag.PreviousPage = page == 0 ? "" : page.ToString();
                ViewBag.Page = page + 1;
                ViewBag.NextPage = _db.Arts.Count() > (page+1) * 20 ? (page + 2).ToString() : "";
            
                return View(thumbnails);
            }

            List<string> rawTags = new List<string>();
            if (tagString != null)
                rawTags.AddRange(tagString.Split(" ").ToList());
            IEnumerable<Art> arts = null;
            
            // If sortBy and orderBy are available then add them
            if (sortBy != null && orderBy != null)
            {
                string tagStr = orderBy + ":" + sortBy;
                rawTags.Add(tagStr);
            }
            
            // >>> Sorting filters <<<
            // author
            IEnumerable<string> artistTags = rawTags.Where(t => t.ToLower().Contains("artist:")).Select(a => a = a.Remove(0, 7));
            if (artistTags.Any())
                arts = _db.Arts.Include(a => a.Author).Include(a => a.Tags)
                    .ThenInclude(t => t.Tag).Include(a => a.Comments).Where(a => artistTags.Contains(a.Author.ArtistName));
            
            
            // include the ones with tags
            Func<Art, bool> tagSearchFunc = a =>
            {
                foreach (var tag in rawTags)
                {
                    // if (!tag.Contains(":") && !a.Tags.Any(t => t.Tag.TagString.Equals(tag)))
                    if (!tag.Contains(":") && !a.Tags.Any(t => t.Tag.TagString.Equals(tag)))
                        return false;
                }

                return true;
            };
                
            if (arts != null)
                arts = arts.Where(tagSearchFunc);
            else
                arts = _db.Arts.Include(a => a.Author).Include(a => a.Tags)
                    .ThenInclude(t => t.Tag).Include(a => a.Comments)
                    .Where(tagSearchFunc);

            
            // by name
            if (rawTags.Count(a => a.ToLower().Equals("orderby:name")) > 0)
                arts = arts.OrderBy(a => a.Name);
            
            
            // by name descending
            if (rawTags.Count(a => a.ToLower().Equals("orderbydesc:name")) > 0)
                arts = arts.OrderByDescending(a => a.Name);
            
            
            // by upload date
            if (rawTags.Count(a => a.ToLower().Equals("orderby:date")) > 0)
                arts = arts.OrderBy(a => a.CreatedAt);
            
            
            // by upload date desc
            if (rawTags.Count(a => a.ToLower().Equals("orderbydesc:date")) > 0)
                arts = arts.OrderByDescending(a => a.CreatedAt);


            // by artist name
            if (rawTags.Count(a => a.ToLower().Equals("orderby:artist")) > 0)
                arts = arts.OrderBy(a => a.Author.ArtistName);

            // by artist name desc
            if (rawTags.Count(a => a.ToLower().Equals("orderbydesc:artist")) > 0)
                arts = arts.OrderByDescending(a => a.Author.ArtistName);
            
            // >>> Pagination <<<
            ViewBag.PreviousPage = page == 0 ? "" : page.ToString();
            ViewBag.Page = page + 1;
            ViewBag.NextPage = arts.Count() > (page+1) * 20 ? (page + 2).ToString() : "";
            ViewBag.TagString = HttpUtility.UrlEncode(tagString);
            
            thumbnails = new List<ArtThumbnailViewModel>();
            
            arts = arts.Skip(page * 20).Take(20);
            foreach (Art a in arts)
                thumbnails.Add(new ArtThumbnailViewModel(a));
            return View(thumbnails);
        }

        [HttpGet]
        public IActionResult Trending()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];

            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PostUploadViewModel model)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            if (ModelState.IsValid)
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    ModelState.AddModelError(string.Empty, "You have to be logged in to upload an art!");
                    return Redirect("/posts/upload");
                }
                
                // Check if the artist exists
                Artist artist = null;
                if (model.Author != null)
                {
                    artist = await _db.Artists
                        .FirstOrDefaultAsync(a => a.ArtistName.Equals(model.Author));
                    if (artist == null)
                    {
                        // TODO: Ask if u can make a link to the artist registration form in here
                        ModelState.AddModelError(string.Empty, $"Could not find artist named {model.Author}." +
                                                               " Please consider adding a new artist <a href=\"#\">here</a>");
                        return View();
                    }
                }

                // Save the files and manage get the necessary data
                string large, normal, thumbnail, hash;
                (int,int) dims;
                long size;
                try
                {
                    using (ImageFileManager ifm = new ImageFileManager("wwwroot/img/posts/", model.File.OpenReadStream(),
    ImageUtils.ImgExtensionFromContentType(model.File.ContentType)))
                    {
                        large = await ifm.SaveLarge();
                        normal = await ifm.SaveNormal();
                        thumbnail = await ifm.SaveThumbnail(0, 0);
                    }
                    hash = ImageUtils.HashFromFile(large);
                    dims = ImageUtils.DimensionsOfImage(large);
                    size = model.File.Length;
                    
                    large = large.Remove(0, 7);
                    normal = normal.Remove(0, 7);
                    thumbnail = thumbnail.Remove(0, 7);
                }
                catch (InvalidArtDimensionsException exception)
                {
                    ModelState.AddModelError(string.Empty, "Invalid art size - the art should be at least 300 x 300px");
                    return View();
                }

                // Get the user data and register unregistered tags
                var usr = await _userManager.GetUserAsync(User);
                List<string> rawTags = model.TagString.Split(' ').ToList();
                List<Tag> tags = new List<Tag>();
                foreach (string rawTag in rawTags)
                {
                    var tag = _db.Tags.FirstOrDefault(t => t.TagString.Equals(rawTag));
                    if (tag == null)
                    {
                        Tag newTag = new Tag {Id = Guid.NewGuid(), Creator = usr, TagString = rawTag, AddedAt = DateTime.Now };
                        await _db.Tags.AddAsync(newTag);
                        tags.Add(newTag);
                    }
                    else
                        tags.Add(tag);

                }
                await _db.SaveChangesAsync();
                
                // Create an art
                Art art = new Art()
                {
                    Id = Guid.NewGuid(),
                    Uploader = usr,
                    Name = model.Name,
                    Source = model.Source,
                    Rating = model.Rating,
                    Author = artist,
                    LargeFileUrl = large,
                    FileUrl = normal,
                    PreviewFileUrl = thumbnail,
                    Md5Hash = hash,
                    Height = dims.Item2,
                    Width = dims.Item1,
                    FileSize = (int) size,
                    Stars = 0,
                    CreatedAt = DateTime.Now,
                };
                
                // Register tag occurrences in the join table and the art
                List<TagOccurrence> occurrences = new List<TagOccurrence>();
                foreach (var tag in tags)
                {
                    occurrences.Add(new TagOccurrence()
                    {
                        Art = art,
                        ArtId = art.Id,
                        Tag = tag,
                        TagId = tag.Id
                    });
                }
                art.Tags = occurrences;

                await _db.Arts.AddAsync(art);
                await _db.SaveChangesAsync();

                return Redirect("/Posts/List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[3];
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Post(string postId)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Artist";

            Art art = _db.Arts.Include(a => a.Author).Include(a => a.Uploader)
                .Include(a => a.Tags).FirstOrDefault(a => a.Id.ToString().Equals(postId));
            if (art == null)
                return Redirect("/posts/list");

            var artist = _db.Artists.First(a => a.Id.ToString().Equals(art.Author.Id.ToString()));
            int artCount = _db.Arts.Include(a => a.Author).Count(a => a.Author.
                Id.ToString().Equals(artist.Id.ToString()));
            int subs = artist.Subscriptions?.Count() ?? 0;
            List<string> tags = new List<string>();
            foreach (var tag in art.Tags)
                tags.Add(_db.Tags.First(t => t.Id.ToString().Equals(tag.TagId.ToString())).TagString);

            bool liked = false;
            if (_signInManager.IsSignedIn(User))
            {
                NeobooruUser usr = await _userManager.GetUserAsync(User);
                liked = _db.ArtLikes.Any(like =>
                    like.User.Id.Equals(usr.Id) && like.LikedArt.Id.ToString().Equals(postId));
            }

            List<CommentViewModel> comments = new List<CommentViewModel>();
            await _db.ArtComments.Where(comment => comment.CommentedArt.Id.ToString().Equals(postId))
                .ForEachAsync(comment =>
                {
                    comments.Add(new CommentViewModel(comment.User.UserName, comment.User.PfpUrl, comment.Content,
                        comment.CommentedOn));
                });
            
            comments.Sort((a, b) => a.Date > b.Date ? -1 : 1);

            return View(new PostViewModel(art, artCount, subs, tags, comments,
                liked));
        }

        [HttpGet]
        public async Task<IActionResult> PostLike(string artId)
        {
            if (!_signInManager.IsSignedIn(User))
                return StatusCode(403);

            NeobooruUser usr = await _userManager.GetUserAsync(User);
            Art targetArt = await _db.Arts.FirstAsync(a => a.Id.ToString().Equals(artId));
            ArtLike like = new ArtLike()
            {
                Id = Guid.NewGuid(),
                LikedArt = targetArt,
                User = usr
            };
            await _db.ArtLikes.AddAsync(like);
            await _db.SaveChangesAsync();
            
            return StatusCode(200);
        }

        [HttpGet]
        public async Task<IActionResult> PostUnlike(string artId)
        {
            if (!_signInManager.IsSignedIn(User))
                return StatusCode(403);

            NeobooruUser usr = await _userManager.GetUserAsync(User);
            ArtLike like = await _db.ArtLikes.FirstAsync(a => a.User.Id.Equals(usr.Id) && 
                                                   a.LikedArt.Id.ToString().Equals(artId));
            _db.ArtLikes.Remove(like);
            await _db.SaveChangesAsync();
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(string CommentString, string artId)
        {
            ArtComment ac = new ArtComment()
            {
                Id = Guid.NewGuid(),
                User = await _userManager.GetUserAsync(User),
                Content = CommentString,
                CommentedOn = DateTime.Now,
                CommentedArt = await _db.Arts.FirstAsync(a => a.Id.ToString().Equals(artId))
            };
            await _db.ArtComments.AddAsync(ac);
            await _db.SaveChangesAsync();
            
            return Redirect("/posts/post?postId=" + artId);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string postId)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit";
            
            Art art = await _db.Arts.Include(a => a.Author)
                .Include(a => a.Tags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(a => a.Id.ToString().Equals(postId));
            if (art == null)
                Redirect("/Posts/List");
            PostEditViewModel puvm = new PostEditViewModel();
            puvm.SecretId = postId;
            puvm.PreviewUrl = art.FileUrl;
            puvm.Author = art.Author.ArtistName;
            puvm.Name = art.Name;
            puvm.Rating = art.Rating;
            puvm.Source = art.Source;
            StringBuilder sb = new StringBuilder();
            foreach (var tag in art.Tags.Select(t => t.Tag.TagString))
                sb.Append(tag.Replace(" ", "_") + " ");
            puvm.TagString = sb.ToString();

            return View(puvm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostEditViewModel puvm)
        {
            // TODO: Edit sometimes causes some key error. Figure out what could be causing it
            NeobooruUser user = await _userManager.GetUserAsync(User);
            Art art = await _db.Arts.Include(a => a.Author)
                .Include(a => a.Tags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(a => a.Id.ToString().Equals(puvm.SecretId));
            
            // Delete old tag occurrences
            foreach (var tag in art.Tags)
                _db.TagOccurrences.Remove(tag);
            await _db.SaveChangesAsync();
            
            // Change to new data
            Artist artist = _db.Artists.FirstOrDefault(a => a.ArtistName.Equals(puvm.Author));
            if (artist == null)
                return Redirect("/Posts/List");
            art.UpdatedAt = DateTime.Now;
            art.Author = artist;
            art.Rating = puvm.Rating;
            art.Source = puvm.Source;
            List<string> passedTags = puvm.TagString.Split(" ").ToList();
            ICollection<TagOccurrence> newTags = new List<TagOccurrence>();
         
            // register unregistered tags
            List<Tag> tagModels = new List<Tag>();
            foreach (var t in passedTags)
            {
                if (!_db.Tags.Any(tag => tag.TagString.Equals(t)))
                {
                    Tag tag = new Tag()
                    {
                        Id = Guid.NewGuid(),
                        TagString = t,
                        AddedAt = DateTime.Now,
                        Creator = user
                    };
                    tagModels.Add(tag);
                    _db.Tags.Add(tag);
                }
                else
                {
                    Tag tag = _db.Tags.First(tag => tag.TagString.Equals(t));
                    tagModels.Add(tag);
                }
            }

            // Register new tag occurrences
            List<TagOccurrence> occurrences = new List<TagOccurrence>();
            foreach (var t in tagModels)
            {
                TagOccurrence to = new TagOccurrence()
                {
                    ArtId = art.Id,
                    TagId = t.Id,
                    Art = art,
                    Tag = t
                };
                occurrences.Add(to);
            }

            art.Tags = occurrences;

            
            // List<TagOccurrence> occurrences = new List<TagOccurrence>();
            // foreach (var t in tagModels)
            // {
            //     TagOccurrence tagOccurrence = _db.TagOccurrences
            //         .FirstOrDefault(to => to.Tag.TagString.Equals(t.TagString));
            //     if (tagOccurrence == null)
            //     {
            //         _db.TagOccurrences.Add(new TagOccurrence()
            //         {
            //             Art = art,
            //             ArtId = art.Id,
            //             Tag = t,
            //             TagId = t.Id
            //         });
            //     }
            //     occurrences.Add(tagOccurrence);
            // }

            await _db.SaveChangesAsync();
            
            return Redirect("/Posts/List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string postId)
        {
            Art art = await _db.Arts.Include(a => a.Tags)
                .Include(a => a.Comments).Include(a => a.Likes)
                .FirstOrDefaultAsync(a => a.Id.ToString().Equals(postId));

            foreach (var like in art.Likes)
                _db.ArtLikes.Remove(like);

            foreach (var comment in art.Comments)
                _db.ArtComments.Remove(comment);

            foreach (var tag in art.Tags)
                _db.TagOccurrences.Remove(tag);
            
            _db.Arts.Remove(art);
            await _db.SaveChangesAsync();
            return Redirect("/Posts/List");
        }
    }
}