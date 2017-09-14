using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shapees.Models;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using Xunit;

//This class tests the Login controller functions

namespace Shapees.Tests
{
    public class UnitTestLoginController
    {
        public ShapeesDB testContext { get; set; }

        //Testing Login page controller function; returns view
        [Fact(DisplayName = "Unit test: Login Controller")]
        public void TestLogin()
        {
            testContext = null;

            var sut = new Shapees.Controllers.LoginController(testContext);

            IActionResult testResult = sut.Index();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Users page controller function; returns view  -   Won't pass as context is private
        [Fact(DisplayName = "Unit test: Users Controller - Will fail as context is private type")]
        public void TestUsers()
        {
            var sut = new Shapees.Controllers.LoginController(testContext);

            IActionResult testResult = sut.Users();

            Assert.IsType<ViewResult>(testResult);
        }
    }
}
