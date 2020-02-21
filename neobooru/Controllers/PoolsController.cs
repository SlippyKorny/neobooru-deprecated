using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neobooru.Models;
using neobooru.ViewModels;

namespace neobooru.Controllers
{
    public class PoolsController : Controller
    {
        private readonly string[] _subsectionPages = { "List", "Create", "Help"};

        [HttpGet]
        public IActionResult List()
        {
            List<PoolThumbnailViewModel> Arts = new List<PoolThumbnailViewModel>();

            Art a1 = new Art(), a2 = new Art(), a3 = new Art();
            a1.PreviewFileUrl = "~/img/prototyping/arts/thumbnails/1-thumbnail.jpg";
            a2.PreviewFileUrl = "~/img/prototyping/arts/thumbnails/2-thumbnail.jpg";
            a3.PreviewFileUrl = "~/img/prototyping/arts/thumbnails/3-thumbnail.jpg";

            Pool pool = new Pool();
            pool.CreatedAt = DateTime.Now;
            pool.PoolName = "Test Pool";
            pool.UpdatedAt = DateTime.Now;
            pool.Arts = new List<Art>();

            pool.Arts.Add(a1);
            pool.Arts.Add(a2);
            pool.Arts.Add(a3);

            for (int i = 0; i < 20; i++)
                Arts.Add(new PoolThumbnailViewModel(pool, "TheSlipper"));

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            return View(Arts);
        }

        [HttpGet]
        public IActionResult Create()
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