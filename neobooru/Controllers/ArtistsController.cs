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

            return View(Artists);
        }
    }
}