using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

//This class tests the HomeController.cs file controllers

namespace Shapees.Tests
{
    public class UnitTestHomeController
    {
        //Testing dashboard/homepage controller function; returns view
        [Fact(DisplayName = "Unit test: Dashboard/Home Controller")]
        public void TestHome()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Index();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Documents and Media Page controller function; returns view
        [Fact(DisplayName = "Unit test: Documents and Media Controller")]
        public void TestDocMedia()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.DocumentsAndMedia();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Documents and Media Page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Documents and Media Controller ViewData['Message']")]
        public void TestDocMediaMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.DocumentsAndMedia();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "DocumentsAndMedia");
        }

        //Testing Schedular page controller function; returns view
        [Fact(DisplayName = "Unit test: Schedular Controller")]
        public void TestSchedular()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Scheduler();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Schedular page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Schedular Controller ViewData['Message']")]
        public void TestSchedularMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.Scheduler();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Scheduler");
        }

        //Testing Announcements page controller function; returns view
        [Fact(DisplayName = "Unit test: Announcements Controller")]
        public void TestAnnouncements()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Announcements();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Announcements page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Announcements Controller ViewData['Message']")]
        public void TestAnnouncementsMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.Announcements();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Announcements");
        }

        //Testing Rooms page controller function; returns view
        [Fact(DisplayName = "Unit test: Rooms Controller")]
        public void TestRooms()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Rooms();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Rooms page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Rooms Controller ViewData['Message']")]
        public void TestRoomsMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.Rooms();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Rooms");
        }

        //Testing Children page controller function; returns view
        [Fact(DisplayName = "Unit test: Children Controller")]
        public void TestChildren()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Children();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Children page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Children Controller ViewData['Message']")]
        public void TestChildrenMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.Children();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Children");
        }

        //Testing Messages page controller function; returns view
        [Fact(DisplayName = "Unit test: Messages Controller")]
        public void TestMessages()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Messages();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Messages page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Messages Controller ViewData['Message']")]
        public void TestMessagesMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.Messages();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Messages");
        }

        //Testing Account Settings controller function; returns view
        [Fact(DisplayName = "Unit test: Account Settings Controller")]
        public void TestSettings()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.AccountSettings();

            Assert.IsType<ViewResult>(testResult);
        }

        //Testing Account Settings page controller function; returns ViewData string
        [Fact(DisplayName = "Unit test: Account Settings Controller ViewData['Message']")]
        public void TestSettingsMessage()
        {
            var sut = new Shapees.Controllers.HomeController();

            var testResult = (ViewResult)sut.AccountSettings();

            var testName = testResult.ViewData["Message"];

            Assert.True(testName == "Account Settings");
        }

        //Testing error controller function; returns view
        [Fact(DisplayName = "Unit test: Error Controller")]
        public void TestError()
        {
            var sut = new Shapees.Controllers.HomeController();

            IActionResult testResult = sut.Error();

            Assert.IsType<ViewResult>(testResult);
        }
    }
}
