using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace neobooru.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Trending()
        {
            return View();
        }

        public IActionResult List()
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