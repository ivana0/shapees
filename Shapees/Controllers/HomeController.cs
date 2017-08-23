using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shapees.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scheduler()
        {
            ViewData["Message"] = "Scheduler";

            return View();
        }

        public IActionResult Announcements()
        {
            ViewData["Message"] = "Announcements";

            return View();
        }

        public IActionResult Documents()
        {
            ViewData["Message"] = "Documents";

            return View();
        }

        public IActionResult Media()
        {
            ViewData["Message"] = "Media";

            return View();
        }

        public IActionResult Rooms()
        {
            ViewData["Message"] = "Rooms";

            return View();
        }

        public IActionResult Children()
        {
            ViewData["Message"] = "Children";

            return View();
        }

        public IActionResult Messages()
        {
            ViewData["Message"] = "Messages";

            return View();
        }

        public IActionResult AccountSettings()
        {
            ViewData["Message"] = "Account Settings";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
