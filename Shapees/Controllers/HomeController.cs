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

namespace Shapees.Controllers
{
    public class HomeController : Controller
    {
        private readonly masterContext _context;

        public HomeController(masterContext context)
        {
            _context = context;
        }

        //  Dashboard/Home page    
        public IActionResult Index()
        {
            //var searchusers = from u in _context.Userinfo.Select(u => u.Room)
                              //select u;
            //additional sets
            //ViewBag.Task = ViewData["User"] = new Userinfo();
            //var task = from t in _context.Userinfo.Select(t => t.Task)
                          // select t;

            //var announcement = from a in _context.Announcement.Select(a => a.Announcementid)
                      // select a;

            //ViewData["Task"] = new SelectList(task, "Taskid", "Taskid");
            //ViewData["Announcement"] = new SelectList(task, "Announcementid", "Announcementid");
            //ViewData["Taskfor"] = new SelectList(task, "Assignedfor", "Assignedfor");

            return View();
        }

        //  Dashboard/Home page for Educator
        public IActionResult IndexEducator()
        {
            return View();
        }
        //  Dashboard/Home page for Parent
        public IActionResult IndexParent()
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
