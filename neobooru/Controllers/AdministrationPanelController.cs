using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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
            var roles = _roleManager.Roles;

            return View(roles);
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
                    return RedirectToAction("ListRoles", "AdministrationPanel");
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[3];
            return View(createRoleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit Role";

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} can not be found!";
                return View("NotFound");
            }

            EditRoleViewModel ervmModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // https://youtu.be/7ikyZk5fGzk
            // foreach(var user in userManager.Users)
            // {
            //      if (await userManager.IsInRoleAsync(user, role.Name))
            //      {
            //          model.Users.Add(user.UserName);
            //      }

            return View(ervmModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel ervmModel)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Edit Role";

            var role = await _roleManager.FindByIdAsync(ervmModel.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {ervmModel.Id} can not be found!";
                return View("NotFound");
            }
            else
            {
                role.Name = ervmModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("ListRoles");
                foreach(var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }

            return View(ervmModel);
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