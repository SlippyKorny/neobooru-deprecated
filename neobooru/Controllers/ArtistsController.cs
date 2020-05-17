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
        private readonly string[] _subsectionPages = { "List", "Register", "Help" };

        [HttpGet]
        public IActionResult List()
        {
            List<ArtistThumbnailViewModel> Artists = new List<ArtistThumbnailViewModel>();

            Artist artist = new Artist()
            {
                ArtistName = "CommieComma",
                RegisteredAt = DateTime.Now,
                ProfileViews = 3231,
                PreviewPfpUrl = "/img/prototyping/artists/CommieComma.png"
            };


            for (int i = 0; i < 20; i++)
                Artists.Add(new ArtistThumbnailViewModel(artist, 23, 532));

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            return View(Artists);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        [HttpGet]
        public IActionResult Artist(Guid artistId)
        {
            List<ArtThumbnailViewModel> list = new List<ArtThumbnailViewModel>();
            Artist artist = new Artist 
            {
                PfpUrl = "~/img/prototyping/artists/CommieComma.png",
                ArtistName = "CommieComma",
                ProfileViews = 3452,
                RegisteredAt = DateTime.Now,
                Country = "Spain",
                TwitterProfileUrl = "https://twitter.com/CommieComma",
                MailAddress = "commieComma@gmail.com",
                Gender = "Male",
                BirthDate = DateTime.Now,
            };

            Art art = new Art();
            art.PreviewFileUrl = "~/img/prototyping/arts/25.png";
            art.Name = "Cute Uraraka";
            art.Author = artist;
            art.CreatedAt = DateTime.Now;

            art.PreviewFileUrl = "~/img/prototyping/arts/24.jpg";
            art.Name = "Cool Camie";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/23.png";
            art.Name = "Cute little sketch of Mina";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/22.png";
            art.Name = "Nejire";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/21.jpg";
            art.Name = "Nejire with ponytail";
            list.Add(new ArtThumbnailViewModel(art));

            ArtistViewModel avm = new ArtistViewModel(artist, list, 345);


            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Artist";

            return View(avm);
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