using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Profile(string profileId)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            if (profileId == null && _signInManager.IsSignedIn(User))
                profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                Redirect("/");

            NeobooruUser user = _db.NeobooruUsers.FirstOrDefault(a => a.Id.Equals(profileId));
            
            if (user == null)
                Redirect("/");

            List<ArtThumbnailViewModel> recentlyUploaded = _db.Arts.Where(a => a.Uploader.Id.Equals(profileId))
                .OrderByDescending(a => a.CreatedAt).Take(5).Select(a => new ArtThumbnailViewModel(a)).ToList();

            return View(new ProfileViewModel(user, recentlyUploaded));
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
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

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

        public async Task<IActionResult> Settings()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            
            if (!_signInManager.IsSignedIn(User))
            {
                ModelState.AddModelError(string.Empty,
                    "You have to be logged in to change your settings!");
                return Redirect("/Profile/Settings");
            }

            NeobooruUser usr = await _userManager.GetUserAsync(User);
            ProfileUpdateViewModel puvm = new ProfileUpdateViewModel()
            {
                Gender = usr.Gender,
                Username = usr.UserName,
                ProfileDescription = usr.ProfileDescription,
            };


            return View(puvm);
        }

        public IActionResult Help()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[2];
            return View();
        }

        public async Task<IActionResult> Update(ProfileUpdateViewModel puvm)
        {
            if (!ModelState.IsValid)
                return Redirect("/Profile/Settings");
            
            if (!_signInManager.IsSignedIn(User))
            {
                ModelState.AddModelError(string.Empty,
                    "You have to be logged in to change your settings!");
                return Redirect("/Profile/Settings");
            }

            NeobooruUser usr = await _userManager.GetUserAsync(User);
            usr.Gender = puvm.Gender;
            usr.UserName = puvm.Username;
            usr.ProfileDescription = puvm.ProfileDescription;

            if (puvm.PfpImage != null)
            {
                Guid id = Guid.NewGuid();

                ImageFileManager ifmPfp = null;
                try
                {
                    ifmPfp = new ImageFileManager("wwwroot/img/profiles/pfps/",
                        puvm.PfpImage.OpenReadStream(), ImageUtils.ImgExtensionFromContentType(puvm.PfpImage.ContentType));
                }
                catch (InvalidArtDimensionsException e)
                {
                    ModelState.AddModelError(string.Empty, "Invalid profile picture or background size! " +
                                                           "Profile picture must be at least 400px by 400px");
                    return Redirect("/Profile/Settings");
                }

                usr.PfpUrl = await ifmPfp.SavePfp(id);
                usr.PfpUrl = usr.PfpUrl.Remove(0, 7);
            }

            if (puvm.BgImage != null)
            {
                Guid id = Guid.NewGuid();

                ImageFileManager ifmPfp = null;
                try
                {
                    ifmPfp = new ImageFileManager("wwwroot/img/profiles/bgs/",
                        puvm.BgImage.OpenReadStream(), ImageUtils.ImgExtensionFromContentType(puvm.BgImage.ContentType));
                }
                catch (InvalidArtDimensionsException e)
                {
                    ModelState.AddModelError(string.Empty, "Invalid profile picture or background size! " +
                                                           "Background must be at least 1590px by 540px");
                    return Redirect("/Profile/Settings");
                }

                usr.BgUrl = await ifmPfp.SaveBg(id);
                usr.BgUrl = usr.BgUrl.Remove(0, 7);
            }

            await _db.SaveChangesAsync();
            return Redirect("/Profile/Profile");
        }
    }
}