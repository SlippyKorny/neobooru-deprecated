using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using neobooru.Models;
using neobooru.ViewModels;
using neobooru.ViewModels.Forms;

namespace neobooru.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly NeobooruDataContext _db;

        private readonly UserManager<NeobooruUser> _userManager;

        private readonly SignInManager<NeobooruUser> _signInManager;

        public ArtistsController(NeobooruDataContext db, UserManager<NeobooruUser> userManager,
            SignInManager<NeobooruUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private readonly string[] _subsectionPages = {"List", "Register", "Help"};

        #region list

        [HttpGet]
        public async Task<IActionResult> List(int page)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];
            
            List<ArtistThumbnailViewModel> artists = new List<ArtistThumbnailViewModel>();
            await _db.Artists.OrderByDescending(a => a.RegisteredAt).Take(20).ForEachAsync(a =>
            {
                int artCount = _db.Arts.Count(b => b.Author.Id.Equals(a.Id));
                int subCount = _db.ArtistSubscriptions.Count(b => b.Artist.Id.Equals(a.Id));
                artists.Add(new ArtistThumbnailViewModel(a, artCount, subCount));
            });

            return View(artists);
        }
        
        #endregion

        #region ArtistRegistration

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ArtistRegistrationViewModel model)
        {
            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[1];

            if (!ModelState.IsValid)
                return View();

            if (!_signInManager.IsSignedIn(User))
            {
                ModelState.AddModelError(string.Empty,
                    "You have to be logged in to register an artist!");
                return View();
            }

            Guid id = Guid.NewGuid();

            ImageFileManager ifmPfp = null, ifmBg = null;
            try
            {
                ifmPfp = new ImageFileManager("wwwroot/img/profiles/pfps/", model.Pfp.OpenReadStream(),
                    ImageUtils.ImgExtensionFromContentType(model.Pfp.ContentType));
                if (model.BackgroundImage != null)
                    ifmBg = new ImageFileManager("wwwroot/img/profiles/bgs/",
                        model.BackgroundImage.OpenReadStream(), 
                        ImageUtils.ImgExtensionFromContentType(model.BackgroundImage.ContentType));
            }
            catch (InvalidArtDimensionsException e)
            {
                ModelState.AddModelError(string.Empty, "Invalid profile picture or background size! " +
                                                       "Profile picture must be at least 400px by 400px and background" +
                                                       "must be at least 1590px by 540px");
                return View();
            }
            string pfp, bg = null;
            pfp = await ifmPfp.SavePfp(id);
            pfp = pfp.Remove(0, 7);
            if (ifmBg != null)
            {
                bg = await ifmBg.SaveBg(id);
                bg = bg.Remove(0, 7);
            }

            Artist artist = new Artist()
            {
                Id = id,
                ArtistName = model.Name,
                RegisteredAt = DateTime.Now,
                RegisteredBy = await _userManager.GetUserAsync(User),
                ProfileViews = 0,
                BackgroundImageUrl = bg,
                PfpUrl = pfp,
                Country = model.Country,
                FacebookProfileUrl = model.FacebookProfileUrl,
                TwitterProfileUrl = model.TwitterProfileUrl,
                MailAddress = model.MailAddress,
                Gender = model.Gender,
            };

            await _db.Artists.AddAsync(artist);
            await _db.SaveChangesAsync();
            
            return Redirect("/Artists/List");
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Artist(Guid artistId)
        {
            List<ArtThumbnailViewModel> list = new List<ArtThumbnailViewModel>();

            Artist artist = await _db.Artists.FirstOrDefaultAsync(a => a.Id.Equals(artistId));
            if (artist == null)
                return Redirect("/Artists/List");

            await _db.Arts.Where(a => a.Author.Id.Equals(artistId)).OrderByDescending(a => a.Stars).Take(5)
                .ForEachAsync(a => list.Add(new ArtThumbnailViewModel(a)));

            IQueryable<ArtistSubscription> follows = _db.ArtistSubscriptions
                .Where(a => a.Artist.Id.Equals(artist.Id));

            ArtistViewModel avm = new ArtistViewModel(artist, list, follows.Count());


            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = "Artist";

            return View(avm);
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