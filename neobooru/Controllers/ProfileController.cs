using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neobooru.ViewModels;

namespace neobooru.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        private SignInManager<IdentityUser> _signInManager;

        private readonly string[] _subsectionPages = { "Profile", "Registration", "Login", "Help" };

        public ProfileController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(BasicUserRegistrationViewModel model)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];

            if (ModelState.IsValid)
            {
                // TODO: Change email -> do not use it as login
                var user = new IdentityUser {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}