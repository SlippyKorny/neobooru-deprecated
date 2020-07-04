using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neobooru.Models;
using neobooru.ViewModels;
using neobooru.ViewModels.Forms;

namespace neobooru.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<NeobooruUser> _userManager;

        private SignInManager<NeobooruUser> _signInManager;

        private readonly NeobooruDataContext _db;
        
        private readonly string[] _subsectionPages = { "Profile", "Settings", "Help"};

        public ProfileController(NeobooruDataContext db, UserManager<NeobooruUser> userManager,
            SignInManager<NeobooruUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Profile(string profileId)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            if (profileId == null && _signInManager.IsSignedIn(User))
                profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                Redirect("/");

            NeobooruUser user = null;
            foreach (var usr in _db.NeobooruUsers.ToArray())
            {
                if (usr.Id.Equals(profileId))
                    user = usr;
            }

            if (user == null)
                Redirect("/");
            
            return View(new ProfileViewModel(user));
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
                NeobooruUser user = new NeobooruUser { 
                    UserName = model.Email, 
                    Email = model.Email, 
                    RegisteredOn = DateTime.Now
                };
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