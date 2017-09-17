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
using Microsoft.AspNetCore.Mvc;
using Shapees.Models;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;


namespace Shapees.Controllers
{
    public partial class LoginController : Controller
    {
        private ShapeesDB usercontext { get; set; }

         public LoginController(ShapeesDB context)
         {
             usercontext = context;
         }

        //  Login page   
        public IActionResult Index()
         {
            return View();

        }

        //  Users page - Admin Only   
        public IActionResult Users()
        {

            return View(usercontext.Users.ToList());

        }

    }
}