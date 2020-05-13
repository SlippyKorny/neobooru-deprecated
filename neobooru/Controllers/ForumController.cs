using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace neobooru.Controllers
{
    public class ForumController : Controller
    {
        private readonly string[] _subsectionPages = { "Index" };

        public IActionResult Index()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            return View();
        }
    }
}