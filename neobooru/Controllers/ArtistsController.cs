using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageManipulation;
using ImageManipulation.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult List()
        {
            List<ArtistThumbnailViewModel> Artists = new List<ArtistThumbnailViewModel>();

            Artist artist = new Artist()
            {
                ArtistName = "CommieComma",
                RegisteredAt = DateTime.Now,
                ProfileViews = 3231,
                PfpUrl = "/img/prototyping/artists/CommieComma.png"
            };


            for (int i = 0; i < 20; i++)
                Artists.Add(new ArtistThumbnailViewModel(artist, 23, 532));

            ViewBag.SubsectionPages = _subsectionPages;
            ViewBag.ActiveSubpage = _subsectionPages[0];

            return View(Artists);
        }

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
                return Redirect("/Artist/Register");
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
            if (ifmBg != null)
                bg = await ifmBg.SaveBg(id);
            
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
                BirthDate = model.BirthDate
            };

            await _db.Artists.AddAsync(artist);
            await _db.SaveChangesAsync();
            
            return Redirect("/Artists/List");
        }

        #endregion

        [HttpGet]
        public IActionResult Artist(Guid artistId)
        {
            List<ArtThumbnailViewModel> list = new List<ArtThumbnailViewModel>();
            Artist artist = new Artist
            {
                PfpUrl = "~/img/prototyping/artists/CommieComma.png",
                ArtistName = "CommieComma",
                ProfileViews = 3452,
                RegisteredAt = DateTime.Now,
                Country = "Spain",
                TwitterProfileUrl = "https://twitter.com/CommieComma",
                MailAddress = "commieComma@gmail.com",
                Gender = "Male",
                BirthDate = DateTime.Now,
            };

            Art art = new Art();
            art.PreviewFileUrl = "~/img/prototyping/arts/25.png";
            art.Name = "Cute Uraraka";
            art.Author = artist;
            art.CreatedAt = DateTime.Now;

            art.PreviewFileUrl = "~/img/prototyping/arts/24.jpg";
            art.Name = "Cool Camie";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/23.png";
            art.Name = "Cute little sketch of Mina";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/22.png";
            art.Name = "Nejire";
            list.Add(new ArtThumbnailViewModel(art));

            art.PreviewFileUrl = "~/img/prototyping/arts/21.jpg";
            art.Name = "Nejire with ponytail";
            list.Add(new ArtThumbnailViewModel(art));

            ArtistViewModel avm = new ArtistViewModel(artist, list, 345);


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