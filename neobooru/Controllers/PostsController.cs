using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using neobooru.Models;
using neobooru.ViewModels;

namespace neobooru.Controllers
{
    public class PostsController : Controller
    {
        private readonly NeobooruDataContext _db;

        private readonly UserManager<NeobooruUser> _userManager;

        private readonly SignInManager<NeobooruUser> _signInManager;

        private readonly string[] _subsectionPages = {"List", "Trending", "Upload", "Help"};

        public PostsController(NeobooruDataContext db, UserManager<NeobooruUser> userManager, SignInManager<NeobooruUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> List(int page)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            
            List<ArtThumbnailViewModel> arts = new List<ArtThumbnailViewModel>();
            await _db.Arts.Include(a => a.Author)
                .OrderByDescending(a => a.CreatedAt).Skip(page * 20).Take(20)
                .ForEachAsync(a => arts.Add(new ArtThumbnailViewModel(a)));

            ViewBag.PreviousPage = page == 0 ? "" : page.ToString();
            ViewBag.Page = page + 1;
            ViewBag.NextPage = _db.Arts.Count() > (page+1) * 20 ? (page + 2).ToString() : "";
            
            return View(arts);
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

                    // TODO: Delete if the above works
                    // var dataSet = _db.Tags.Where(t => t.TagString.Equals(rawTag));
                    // if (dataSet.Count() == 0)
                    // {
                    //
                    // }
                    // else
                    //     tags.Add(dataSet.First());
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
        public IActionResult Post(string postId)
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
            
            return View(new PostViewModel(art, artCount, subs, tags));
        }
    }
}