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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Shapees.Models;
using Shapees.Models.DatabaseModel;

namespace Shapees.Controllers
{
    public class HomeController : Controller
    {
        public masterContext _context { get; set; }

        public HomeController(masterContext context)
        {
            _context = context;
        }

        //  Dashboard/Home page    
        public IActionResult Index()
        {
            //get key from session
            //uncomment when website editing/testing is finished
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session, redirect
            if (sesKey == null)
            {
                return Redirect("NoAdmittance");
            }*/

            return View();
        }

        //  Documents and Media page  
        public IActionResult DocumentsAndMedia()
        {
            //get key from session
            //uncomment when website editing/testing is finished
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session, redirect
            if (sesKey == null)
            {
                return Redirect("NoAdmittance");
            }*/

            ViewData["Message"] = "DocumentsAndMedia";

            return View();
        }

        //  Scheduler page 
        public IActionResult Scheduler()
        {
            //remove uncommented data and uncomment when webpage editing/testing is done
            //get key from session
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session
            if (sesKey != null)
            {
                //get uID from session
                int? curId = HttpContext.Session.GetInt32("uID");
                //if error or session key bypass, restrict access
                if(curId == null)
                {
                    return Redirect("NoAdmittance");
                }

                //get quick user credentials
                var currentUser = _context.Userinfo.SingleOrDefault(u => u.Userid == curId);
                //double check for errors
                if(currentUser == null)
                {
                    return Redirect("NoAdmittance");
                }

                //check user type (if parent, redirect)
                if(currentUser.Usertype == 1)
                {
                    return Redirect("NoAdmittance");
                }

                ViewData["Message"] = "Scheduler";

                return View();
            }
            else
            {
                return Redirect("NoAdmittance");
            }*/

            ViewData["Message"] = "Scheduler";

            return View();

        }

        //  Announcements page - Admin Only
        public IActionResult Announcements()
        {
            //remove uncommented data and uncomment when webpage editing/testing is done
            //get key from session
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session
            if (sesKey != null)
            {
                //get uID from session
                int? curId = HttpContext.Session.GetInt32("uID");
                //if error or session key bypass, restrict access
                if (curId == null)
                {
                    return Redirect("NoAdmittance");
                }

                //get quick user credentials
                var currentUser = _context.Userinfo.SingleOrDefault(u => u.Userid == curId);
                //double check for errors
                if (currentUser == null)
                {
                    return Redirect("NoAdmittance");
                }

                //check user type (if not director, redirect)
                if (currentUser.Usertype != 3)
                {
                    return Redirect("NoAdmittance");
                }

                ViewData["Message"] = "Announcements";

                return View();
            }
            else
            {
                return Redirect("NoAdmittance");
            }*/

            ViewData["Message"] = "Announcements";

            return View();
        }

        //  Rooms page              
        public IActionResult Rooms()
        {
            //remove uncommented data and uncomment when webpage editing/testing is done
            //get key from session
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session
            if (sesKey != null)
            {
                //get uID from session
                int? curId = HttpContext.Session.GetInt32("uID");
                //if error or session key bypass, restrict access
                if(curId == null)
                {
                    return Redirect("NoAdmittance");
                }

                //get quick user credentials
                var currentUser = _context.Userinfo.SingleOrDefault(u => u.Userid == curId);
                //double check for errors
                if(currentUser == null)
                {
                    return Redirect("NoAdmittance");
                }

                //check user type (if parent, redirect)
                if(currentUser.Usertype == 1)
                {
                    return Redirect("NoAdmittance");
                }

                ViewData["Message"] = "Rooms";

                return View();
            }
            else
            {
                return Redirect("NoAdmittance");
            }*/

            ViewData["Message"] = "Rooms";

            return View();
        }

        //  Children page            
        public IActionResult Children()
        {
            //remove uncommented data and uncomment when webpage editing/testing is done
            //get key from session
            /*int? sesKey = HttpContext.Session.GetInt32("key");

            //check if there a no session
            if (sesKey != null)
            {
                //get uID from session
                int? curId = HttpContext.Session.GetInt32("uID");
                //if error or session key bypass, restrict access
                if(curId == null)
                {
                    return Redirect("NoAdmittance");
                }

                //get quick user credentials
                var currentUser = _context.Userinfo.SingleOrDefault(u => u.Userid == curId);
                //double check for errors
                if(currentUser == null)
                {
                    return Redirect("NoAdmittance");
                }

                //check user type (if parent, redirect)
                if(currentUser.Usertype == 1)
                {
                    ViewData["Message"] = "YourChild(ren)";
                }
                else
                {
                    ViewData["Message"] = "Children";
                }
                
                return View();
            }
            else
            {
                return Redirect("NoAdmittance");
            }*/

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


        //  NoAdmittance page
        public IActionResult NoAdmittance()
        {
            return View();
        }


    }
}
