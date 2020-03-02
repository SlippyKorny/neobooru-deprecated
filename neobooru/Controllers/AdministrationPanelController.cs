using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace neobooru.Controllers
{
    // remember that u can override this with [AllowAnonymous]
    [Authorize]
    public class AdministrationPanelController : Controller
    {
        
        private readonly string[] _subsectionPages = { "Main Panel", "List Roles", "Help" };


        [HttpGet]
        public IActionResult MainPanel()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            return View();
        }

        [HttpGet]
        public IActionResult ListRoles()
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

        [HttpGet]
        public IActionResult EditUser()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit User";
            return View();
        }

        [HttpGet]
        public IActionResult EditRole()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit Role";
            return View();
        }
    }
}