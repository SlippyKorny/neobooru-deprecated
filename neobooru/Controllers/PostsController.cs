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

        public PostsController(ApplicationDbContext db)
        {
            _db = db;
            
        }

        // GET: posts/List
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

            Arts.Add(new ArtThumbnailViewModel(a1));
            Arts.Add(new ArtThumbnailViewModel(a1));
            Arts.Add(new ArtThumbnailViewModel(a1));
            Arts.Add(new ArtThumbnailViewModel(a1));
            Arts.Add(new ArtThumbnailViewModel(a1));
            return View(Arts);
        }

        public IActionResult Trending()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}