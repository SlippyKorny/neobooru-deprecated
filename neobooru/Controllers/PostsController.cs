using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neobooru.Models;
using neobooru.ViewModels;

namespace neobooru.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly string[] _subsectionPages = {"List", "Trending", "Upload", "Help"};

        public PostsController(ApplicationDbContext db)
        {
            _db = db;
            
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

            a1.tags = new List<Tag>();
            tag1.Type = Tag.TagType.Character;
            tag1.TagString = "Ishtar (fate)";
            a1.tags.Add(tag1);
            tag3.Type = Tag.TagType.Series;
            tag3.TagString = "fate/grand order";
            a1.tags.Add(tag3);

            for (int i = 0; i < 8; i++)
            {
                tag2.Type = Tag.TagType.Classic;
                tag2.TagString = "1 girl";
                a1.tags.Add(tag2);
                tag4.Type = Tag.TagType.Classic;
                tag4.TagString = "black hair";
                a1.tags.Add(tag4);
                tag5.Type = Tag.TagType.Classic;
                tag5.TagString = "breasts";
                a1.tags.Add(tag5);
            }

            a1.Id = Guid.NewGuid();
            a1.Source = "https://twitter.com/mituk1/status/1231496990896189441";
            a1.Name = "Test art!";
            a1.Author = artist;
            a1.CreatedAt = DateTime.Now;
            a1.PreviewFileUrl = "~/img/prototyping/arts/26.jpg";
            a1.FileUrl = a1.PreviewFileUrl;
            a1.LargeFileUrl = a1.FileUrl;
            a1.Uploader = "TheSlipper";
            a1.Width = 845;
            a1.Height = 1200;

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Artist";

            return View(new PostViewModel(a1, artist, 78, 224));
        }
    }
}