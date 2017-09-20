using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.TestModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Shapees.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly testMasterContext _context;

        public UserProfileController(testMasterContext context)
        {
            _context = context;    
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Userprofile.Include(u => u.Address).Include(u => u.User);

            return View(await masterContext.ToListAsync());
        }

        // GET: UserProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userprofile = await _context.Userprofile
                .Include(u => u.Address)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.ProfileId == id);
            if (userprofile == null)
            {
                return NotFound();
            }

            //added
            var filename = userprofile.HomePhone;

            string path = FileStrem.GetFilePath("wwwroot/uploads/profilepictures/" + filename);
            byte[] imagebyte = LoadImage.GetPictureData(path);
            var base64 = Convert.ToBase64String(imagebyte);
            ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
            ViewBag.imagelegth = base64.Length;

            return View(userprofile);
        }
        //added
        public static class LoadImage
        {

            public static byte[] GetPictureData(string imagePath)
            {
                FileStream fs = new FileStream(imagePath, FileMode.Open);
                byte[] byteData = new byte[fs.Length];
                fs.Read(byteData, 0, byteData.Length);
                return byteData;
            }
        }
        //added
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

        // GET: UserProfile/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "City");
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Email");
            return View();
        }

        // POST: UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,FirstName,LastName,Dob,HomePhone,MobilePhone,AddressId,Userid,Profileimage")] Userprofile userprofile, IFormFile file)
        {

            if (file == null || file.Length == 0)
                return Content("file not selected");

            var filename = userprofile.Userid.ToString() + file.FileName;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/profilepictures",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (ModelState.IsValid)
            {

                userprofile.HomePhone = file.FileName;

                _context.Add(userprofile);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", userprofile.AddressId);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", userprofile.Userid);
            return View(userprofile);
        }

        // GET: UserProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userprofile = await _context.Userprofile.SingleOrDefaultAsync(m => m.ProfileId == id);
            if (userprofile == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", userprofile.AddressId);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", userprofile.Userid);
            return View(userprofile);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,FirstName,LastName,Dob,HomePhone,MobilePhone,AddressId,Userid,Profileimage")] Userprofile userprofile)
        {
            if (id != userprofile.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userprofile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserprofileExists(userprofile.ProfileId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", userprofile.AddressId);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "UserId", userprofile.Userid);
            return View(userprofile);
        }

        // GET: UserProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userprofile = await _context.Userprofile
                .Include(u => u.Address)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.ProfileId == id);
            if (userprofile == null)
            {
                return NotFound();
            }

            return View(userprofile);
        }

        // POST: UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userprofile = await _context.Userprofile.SingleOrDefaultAsync(m => m.ProfileId == id);
            _context.Userprofile.Remove(userprofile);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserprofileExists(int id)
        {
            return _context.Userprofile.Any(e => e.ProfileId == id);
        }


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
