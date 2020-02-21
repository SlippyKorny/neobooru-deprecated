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

        // public IEnumerable<Art> Arts { get; set; }

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
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            return View();
        }
    }
}