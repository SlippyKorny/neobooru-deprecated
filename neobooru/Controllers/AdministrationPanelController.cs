using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using neobooru.ViewModels.Forms;

namespace neobooru.Controllers
{
    // remember that u can override this with [AllowAnonymous]
    // [Authorize]
    public class AdministrationPanelController : Controller
    {
        
        private readonly string[] _subsectionPages = { "Main Panel", "List Users", "List Roles", "Create Role", "Help" };
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationPanelController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult MainPanel()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            return View();
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
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
        public IActionResult ListRoles()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[3];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole 
                {
                    Name = createRoleViewModel.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                    return RedirectToAction("MainPanel", "AdministrationPanel");
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }


            return View(createRoleViewModel);
        }

        [HttpGet]
        public IActionResult EditRole()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit Role";
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[4];
            return View();
        }
    }
}