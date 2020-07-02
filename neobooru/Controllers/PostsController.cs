using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
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
        public IActionResult List()
        {
            List<ArtThumbnailViewModel> Arts = new List<ArtThumbnailViewModel>();

            Artist artist = new Artist();
            artist.ArtistName = "TheSlipper";
            Art a1 = new Art();
            a1.Name = "Test art!";
            a1.Author = artist;
            a1.CreatedAt = DateTime.Now;
            a1.PreviewFileUrl = "~/img/prototyping/arts/20.png";

            for (int i = 0; i < 20; i++)
                Arts.Add(new ArtThumbnailViewModel(a1));

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            return View(Arts);
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
                // if (model.File == null)
                // {
                    // ModelState.AddModelError(string.Empty, "No image was chosen!");
                    // return Redirect("/posts/upload");
                // }
                
                if (!_signInManager.IsSignedIn(User)) // TODO: Check if this works
                {
                    ModelState.AddModelError(string.Empty, "You have to be logged in to upload an art!");
                    return Redirect("/posts/upload");
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
                        normal = await ifm.Save();
                        thumbnail = await ifm.SaveThumbnail(0, 0);
                    }
                    hash = ImageUtils.HashFromFile(large);
                    dims = ImageUtils.DimensionsOfImage(large);
                    size = model.File.Length;
                }
                catch (InvalidArtDimensionsException exception)
                {
                    ModelState.AddModelError(string.Empty, "Invalid art size - the art should be at least 300 x 300px");
                    return View();
                }

                // Get the user data and extract the tag data (and create tags if are the given ones are new)
                var usr = await _userManager.GetUserAsync(User);
                List<string> rawTags = model.TagString.Split(' ').ToList();
                List<Tag> tags = new List<Tag>();
                foreach (string rawTag in rawTags)
                {
                    var dataSet = _db.Tags.Where(t => t.TagString.Equals(rawTag));
                    if (dataSet.Count() == 0)
                    {
                        Tag newTag = new Tag {Id = Guid.NewGuid(), Creator = usr, TagString = rawTag, AddedAt = DateTime.Now };
                        await _db.Tags.AddAsync(newTag);
                        tags.Add(newTag);
                    }
                    else
                        tags.Add(dataSet.First());
                }
                await _db.SaveChangesAsync();

                // Put the data in the model and save it to the database
                // TODO: Get the artist
                // Art art = new Art(model, usr, null, tags, large, normal, thumbnail, hash, dims.Item2, dims.Item1, (int)size);
                Art art = new Art()
                {
                    Id = Guid.NewGuid(),
                    Uploader = usr,
                    Name = model.Name,
                    Source = model.Source,
                    Rating = model.Rating,
                    Author = null,
                    Tags = tags,
                    LargeFileUrl = large,
                    FileUrl = normal,
                    PreviewFileUrl = thumbnail,
                    Md5Hash = hash,
                    Height = dims.Item2,
                    Width = dims.Item1,
                    FileSize = (int) size,
                    Stars = 0,
                    CreatedAt = DateTime.Now
                };
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
        public IActionResult Post(Guid postId)
        {
            Tag tag1 = new Tag(), tag2 = new Tag(), tag3 = new Tag(), tag4 = new Tag(), tag5 = new Tag();
            Artist artist = new Artist();
            Art a1 = new Art();

            artist.ArtistName = "yano mitsuki";
            artist.PreviewPfpUrl = "~/img/prototyping/artists/yanoMitsuki.jpg";
            artist.RegisteredAt = DateTime.Now;
            artist.ProfileViews = 3234;

            a1.Tags = new List<Tag>();
            tag1.TagString = "Ishtar (fate)";
            a1.Tags.Add(tag1);
            tag3.TagString = "fate/grand order";
            a1.Tags.Add(tag3);

            for (int i = 0; i < 8; i++)
            {
                tag2.TagString = "1 girl";
                a1.Tags.Add(tag2);
                tag4.TagString = "black hair";
                a1.Tags.Add(tag4);
                tag5.TagString = "breasts";
                a1.Tags.Add(tag5);
            }

            a1.Id = Guid.NewGuid();
            a1.Source = "https://twitter.com/mituk1/status/1231496990896189441";
            a1.Name = "Test art!";
            a1.Author = artist;
            a1.CreatedAt = DateTime.Now;
            a1.PreviewFileUrl = "~/img/prototyping/arts/26.jpg";
            a1.FileUrl = a1.PreviewFileUrl;
            a1.LargeFileUrl = a1.FileUrl;
            a1.Uploader = new NeobooruUser();
            a1.Width = 845;
            a1.Height = 1200;

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Artist";

            return View(new PostViewModel(a1, artist, 78, 224));
        }
    }
}