using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neobooru.Models;
using neobooru.ViewModels;

namespace neobooru.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly string[] _subsectionPages = { "List", "Submit", "Help" };

        [HttpGet]
        public IActionResult List()
        {
            List<ArtistThumbnailViewModel> Artists = new List<ArtistThumbnailViewModel>();

            Artist artist = new Artist();
            artist.ArtistName = "CommieComma";
            artist.RegisteredAt = DateTime.Now;
            artist.ProfileViews = 3231;
            artist.PreviewPfpUrl = "/img/prototyping/artists/CommieComma.png";

            for (int i = 0; i < 20; i++)
                Artists.Add(new ArtistThumbnailViewModel(artist, 23, 532));

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            return View(Artists);
        }

        [HttpGet]
        public IActionResult Submit()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            return View();
        }
    }
}