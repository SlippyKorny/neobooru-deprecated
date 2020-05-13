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

        private readonly string[] _subsectionPages = { "Profile View", "Settings", "Help"};

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
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Profile", "Profile");
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(BasicUserRegistrationViewModel model)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Profile", "Profile");

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            if (ModelState.IsValid)
            {
                // TODO: Change email -> do not use it as login/username
                IdentityUser user = new IdentityUser {UserName = model.Email, Email = model.Email};
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Profile", "Profile");
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Login";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Profile", "Profile");

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // TODO: This doesn't redirect to the returnUrl despite populating the string variable
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Settings()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        public IActionResult Help()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            return View();
        }
    }
}