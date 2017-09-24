using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.DatabaseModel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Shapees.Controllers.DatabaseModelControllers
{
    public class UserinfoDBController : Controller
    {
        private readonly masterContext _context;

        public UserinfoDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: UserinfoDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Userinfo.Include(u => u.Room);
            return View(await masterContext.ToListAsync());
        }

        // GET: UserinfoDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                .Include(u => u.Room)
                .SingleOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            //added for image load
            var filename = userinfo.Profileimage;

            string path = FileStrem.GetFilePath("wwwroot/uploads/profilepictures/" + filename);
            byte[] imagebyte = LoadImage.GetPictureData(path);
            var base64 = Convert.ToBase64String(imagebyte);
            ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
            ViewBag.imagelegth = base64.Length;

            return View(userinfo);
        }

        //added for image load
        public static class LoadImage
        {

            public static byte[] GetPictureData(string imagePath)
            {
                FileStream fs = new FileStream(imagePath, FileMode.Open);
                byte[] byteData = new byte[fs.Length];
                fs.Read(byteData, 0, byteData.Length);
                //hopefully fixed process issue
                fs.Dispose();
                return byteData;
            }
        }
        //added for image load
        public static class FileStrem
        {
            /// <summary>
            /// get absolute path
            /// </summary>
            /// <param name="relativepath">"TextFile.txt"</param>
            /// <returns></returns>
            public static string GetFilePath(string relativepath)
            {
                return Path.Combine(Directory.GetCurrentDirectory(), relativepath);
            }
        }

        // GET: UserinfoDB/Create
        public IActionResult Create()
        {
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid");
            return View();
        }

        // POST: UserinfoDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Email,Pass,Usertype,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo, IFormFile file)
        {
            //file handling
            if (file == null || file.Length == 0)
            {
                //return Content("file not selected");
                return RedirectToAction("Create");
            }

            var filename = userinfo.Username + file.FileName;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/profilepictures",
                        filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (ModelState.IsValid)
            {
                //initialize islogged in variable (-1 for not logged in, 0 for logged in)
                userinfo.Isloggedin = -1;
                //set user's profile image path
                userinfo.Profileimage = filename;

                _context.Add(userinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", userinfo.Roomid);
            return View(userinfo);
        }

        // GET: UserinfoDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", userinfo.Roomid);
            return View(userinfo);
        }

        // POST: UserinfoDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Username,Email,Pass,Usertype,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo)
        {
            if (id != userinfo.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(userinfo.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", userinfo.Roomid);
            return View(userinfo);
        }

        // GET: UserinfoDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                .Include(u => u.Room)
                .SingleOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // POST: UserinfoDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == id);
            _context.Userinfo.Remove(userinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserinfoExists(int id)
        {
            return _context.Userinfo.Any(e => e.Userid == id);
        }

        //FILE UPLOAD/DOWNLOAD
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            throw new NotImplementedException();
        }
    }
}
