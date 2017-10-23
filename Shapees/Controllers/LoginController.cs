/* ---------------------------------- SHAPEES 2017 ---------------------------------- */
/*                                                                                    */
/* Created and Edited by Four Corner Solutions team:                                  */
/* Ivana Ozakovic, Nicole Lardner, Damon Walker,                                      */
/* Cassandra Kalabric, Matthew Mauri, Sima Narain                                     */
/*                                                                                    */
/* Created for Kid's Uni, University of Wollongong, Wollongong                        */
/*                                                                                    */
/* Filename: Controllers/LoginController.cs                                           */
/*                                                                                    */
/* File description: Login pages controller file.                                     */
/* ---------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Shapees.Models;
using Shapees.Models.DatabaseModel;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;


namespace Shapees.Controllers
{
    public partial class LoginController : Controller
    {
        private masterContext _context { get; set; }

        public LoginController(masterContext context)
        {
            _context = context;
        }

        //  Login page   
        public IActionResult Index()
        {
            //clear session details if they exist
            int? curSes = HttpContext.Session.GetInt32("key");
            if (curSes != null)
            {
                curSes = HttpContext.Session.GetInt32("uID");
                if (curSes != null)
                {
                    var logoutUser = _context.Userinfo.SingleOrDefault(u => u.Userid == curSes);
                    if (logoutUser != null)
                    {
                        //set logoff state for user
                        logoutUser.Isloggedin = -1;
                        logoutUser.Lastlogin = DateTime.Now;
                        _context.SaveChanges();
                    }

                    HttpContext.Session.Clear();
                }
            }

            return View();
        }

        //  Login function
        [HttpPost]
        public IActionResult Login()
        {
            if (ModelState.IsValid)
            {
                string inputUsername = HttpContext.Request.Form["inputUsername"];
                string inputPassword = HttpContext.Request.Form["inputPassword"];

                // check username
                var testUsers = _context.Userinfo.SingleOrDefault(u => u.Username.Equals(inputUsername));
                if (testUsers == null)
                {
                    testUsers = _context.Userinfo.SingleOrDefault(u => u.Email.Equals(inputUsername));
                    if (testUsers == null)
                    {
                        return Redirect("Index");
                    }
                }

                Console.WriteLine("username or email = '" + testUsers.Username.Trim() + "' and password = '" + testUsers.Pass.Trim() + "'");

                // check password
                if (testUsers.Pass.Trim() == inputPassword)
                {
                    //update database
                    testUsers.Isloggedin = 0;
                    _context.SaveChanges();

                    //create session
                    //create random number session key
                    byte[] keyset = new byte[8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(keyset);
                    }
                    HttpContext.Session.Set("key", keyset);
                    //set userID to be carried over
                    HttpContext.Session.SetInt32("uID", testUsers.Userid);
                    //set role to be carried over
                    HttpContext.Session.SetInt32("role", testUsers.Usertype);
                    //set user as first name display (for "Welcome ...")
                    string shortName = testUsers.Firstname;
                    HttpContext.Session.SetString("shortName", shortName);
                    //set dashboard name as full name display
                    string fullName = testUsers.Firstname + " " + testUsers.Lastname;
                    HttpContext.Session.SetString("fullName", fullName);

                    /*var data = new byte[] { 1, 2, 3, 4 };
                    HttpContext.Session.Set("key", data); // store byte array
                    HttpContext.Session.TryGetValue("key", out data); // read from session
                    HttpContext.Session.SetString("test", "data as string"); // store string
                    HttpContext.Session.SetInt32("number", 4711); // store int*/

                    return Redirect("~/Home/Index");
                }
                else
                {
                    return Redirect("Index");
                }
            }
            else
            {
                return Redirect("Index");
            }
        }

        //  Users page - Admin Only   
        public IActionResult Users()
        {

            return View(_context.Userinfo.ToList());

        }

    }
}