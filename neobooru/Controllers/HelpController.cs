using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace neobooru.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}