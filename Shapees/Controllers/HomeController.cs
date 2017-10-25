/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Controllers/HomeController.cs                                            */
/*                                                                                    */
/* File description: Home pages controller file.                                      */
/* ---------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shapees.Models.DatabaseModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shapees.Controllers.DatabaseModelControllers;

namespace Shapees.Controllers
{
    public class HomeController : Controller
    {
        private readonly masterContext _context;

        public HomeController(masterContext context)
        {
            _context = context;
        }

        // GET: UserinfoDB/Details/5
        public async Task<IActionResult> Index()
        {

            var userID = HttpContext.Session.GetInt32("uID");
            if (userID == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                //.AsNoTracking()
                .Include(u => u.Room)
                .Include(u => u.Task)
                .SingleOrDefaultAsync(u => u.Userid == userID);

            if (userinfo == null)
            {
                return NotFound();
            }

            if (userinfo.Profileimage != null)
            {
                //added for image load
                var filename = userinfo.Profileimage;


                string path = FileStrem.GetFilePath("wwwroot/uploads/profilepictures/" + filename);
                byte[] imagebyte = LoadImage.GetPictureData(path);
                var base64 = Convert.ToBase64String(imagebyte);
                ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);

            }


            return View(userinfo);
        }

        //  Dashboard/Home page for Educator
        public async Task<IActionResult> IndexEducator()
        {

            var userID = HttpContext.Session.GetInt32("uID");
            if (userID == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                //.AsNoTracking()
                .Include(u => u.Room)
                .Include(u => u.Task)
                .SingleOrDefaultAsync(u => u.Userid == userID);

            if (userinfo == null)
            {
                return NotFound();
            }

            if (userinfo.Profileimage != null)
            {
                //added for image load
                var filename = userinfo.Profileimage;

                string path = FileStrem.GetFilePath("wwwroot/uploads/profilepictures/" + filename);
                byte[] imagebyte = LoadImage.GetPictureData(path);
                var base64 = Convert.ToBase64String(imagebyte);
                ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);

            }


            return View(userinfo);
        }

        //  Dashboard/Home page for Parent
        public async Task<IActionResult> IndexParent()
        {

            var userID = HttpContext.Session.GetInt32("uID");
            if (userID == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo.SingleOrDefaultAsync(u => u.Userid == userID);

            if (userinfo == null)
            {
                return NotFound();
            }

            if (userinfo.Profileimage != null)
            {
                //added for image load
                var filename = userinfo.Profileimage;

                string path = FileStrem.GetFilePath("wwwroot/uploads/profilepictures/" + filename);
                byte[] imagebyte = LoadImage.GetPictureData(path);
                var base64 = Convert.ToBase64String(imagebyte);
                ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);

            }


            return View(userinfo);
        }

        //  Documents and Media page  
        public IActionResult DocumentsAndMedia()
        {
            ViewData["Message"] = "DocumentsAndMedia";

            return View();
        }

        //  Scheduler page 
        public IActionResult Scheduler()
        {
            ViewData["Message"] = "Scheduler";

            return View();
        }

        //  Announcements page - Admin Only
        public IActionResult Announcements()
        {
            ViewData["Message"] = "Announcements";

            return View();
        }

        //  Rooms page              
        public IActionResult Rooms()
        {
            ViewData["Message"] = "Rooms";

            return View();
        }

        //  Children page            
        public IActionResult Children()
        {
            ViewData["Message"] = "Children";

            return View();
        }

        //  Messages page
        public IActionResult Messages()
        {
            ViewData["Message"] = "Messages";

            return View();
        }

        //  Account Settings page
        public IActionResult AccountSettings()
        {
            ViewData["Message"] = "Account Settings";

            return View();
        }

        //  Error page
        public IActionResult Error()
        {
            return View();
        }


    }
}
