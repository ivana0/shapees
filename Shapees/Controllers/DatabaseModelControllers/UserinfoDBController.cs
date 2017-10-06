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
using System.Text;
using System.Security.Cryptography;

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
        public async Task<IActionResult> Index(string searchString, string searchuser, string sortOrder)
        {
            //sorting filters
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewData["SurnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "surname_asc" : "";
            ViewData["TypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "type_asc" : "";
            ViewData["CurrentFilter"] = searchString;

            var searchusers = from u in _context.Userinfo.Include(u => u.Room)
                              select u;

            //search users
            if (!String.IsNullOrEmpty(searchString) && searchuser == "byfirstlast")
            {
                string[] words = searchString.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in words)
                {
                    searchusers = searchusers.Where(s => s.Firstname.Contains(str) || s.Lastname.Contains(str));
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchuser == "bytype")
            {
                searchusers = searchusers.Where(s => s.Usertypename.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_asc":
                    searchusers = searchusers.OrderBy(s => s.Firstname);
                    break;
                case "surname_asc":
                    searchusers = searchusers.OrderBy(s => s.Lastname);
                    break;
                case "type_asc":
                    searchusers = searchusers.OrderBy(s => s.Usertypename);
                    break;
                default:
                    searchusers = searchusers.OrderBy(s => s.Firstname);
                    break;
            }

            //var masterContext = _context.Userinfo.Include(u => u.Room);
            return View(await searchusers.ToListAsync());
        }

        // GET: UserinfoDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                //.AsNoTracking()
                .Include(u => u.Room)
                .SingleOrDefaultAsync(m => m.Userid == id);
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
                ViewBag.imagelegth = base64.Length;
            }
            

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
        public async Task<IActionResult> Create([Bind("Userid,Username,Email,Pass,Usertype,Usertypename,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Roomid,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo, IFormFile file)
        {
            //file handling
            if (file == null || file.Length == 0)
            {
                userinfo.Profileimage = null;
            }
            else
            {
                var filename = userinfo.Username.Trim() + file.FileName;
                //set user's profile image filename
                userinfo.Profileimage = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/profilepictures",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            //get room info to display
            var room = await _context.Room.SingleOrDefaultAsync(m => m.Roomid == userinfo.Roomid);

            if (ModelState.IsValid)
            {
                //do not assign rooms to parent and director users
                if (userinfo.Roomid == 1 && (userinfo.Usertype == 3 || userinfo.Usertype == 1))
                {
                    userinfo.Roomid = null;
                    userinfo.Roomassigned = null;
                }

                //make sure to remove unecessary info for parent
                if (userinfo.Usertype == 1)
                {
                    userinfo.Shortbio = null;
                    userinfo.Employedon = null;
                }

                //make sure to remove unecessary parent info for educators and director
                if (userinfo.Usertype == 2 || userinfo.Usertype == 3)
                {
                    userinfo.Othercontact = null;
                }

                //set room name for educators
                if (userinfo.Roomid == 1 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() +")";  
                if (userinfo.Roomid == 2 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() + ")";
                if (userinfo.Roomid == 3 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() + ")";

                //set user type name
                if (userinfo.Usertype == 1)
                    userinfo.Usertypename = "Parent";
                if (userinfo.Usertype == 2)
                    userinfo.Usertypename = "Educator";
                if (userinfo.Usertype == 3)
                    userinfo.Usertypename = "Director";

                //initialize islogged in variable (-1 for not logged in, 0 for logged in)
                userinfo.Isloggedin = -1;

                //crypt the given password
                //declare var with sha hash method
                var sha = SHA256.Create();
                //hash using given password and created salt
                var hashing = sha.ComputeHash(Encoding.UTF8.GetBytes(userinfo.Pass.ToString()));
                var hashed = BitConverter.ToString(hashing).Replace("-", "").ToLower();

                //change password to hashed version
                userinfo.Pass = hashed;

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
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Username,Email,Pass,Usertype,Usertypename,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Roomid,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo, IFormFile file)
        {
            if (id != userinfo.Userid)
            {
                return NotFound();
            }

            //file handling
            if (file == null || file.Length == 0)
            {
                //return Content("file not selected");
                //return RedirectToAction("Edit");
                userinfo.Profileimage = userinfo.Profileimage;
            }
            else {

                var filename = userinfo.Username.Trim() + file.FileName;

                userinfo.Profileimage = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/profilepictures",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            //get room info to display
            var room = await _context.Room.SingleOrDefaultAsync(m => m.Roomid == userinfo.Roomid);

            if (ModelState.IsValid)
            {

                //set room name for educators
                if (userinfo.Roomid == 1 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() + ")";
                if (userinfo.Roomid == 2 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() + ")";
                if (userinfo.Roomid == 3 && userinfo.Usertype == 2)
                    userinfo.Roomassigned = room.Roomname.Trim() + " (" + room.Roomagegroup.Trim() + ")";

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
