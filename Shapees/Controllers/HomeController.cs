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

namespace Shapees.Controllers
{
    public class HomeController : Controller
    {
        //  Dashboard/Home page     
        public IActionResult Index()
        {
            return View();
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
