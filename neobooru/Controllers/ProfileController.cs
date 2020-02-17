using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace neobooru.Controllers
{
    public class ProfileController : Controller
    {
        // GET: profile/myprofile
        public IActionResult MyProfile()
        {
            return View();
        }
    }
}