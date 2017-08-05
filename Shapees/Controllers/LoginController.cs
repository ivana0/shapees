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

         public IActionResult Index()
         {
            return View();
            //return View();

        }

        public IActionResult Users()
        {
            return View(usercontext.Users.ToList());

        }
    }
}