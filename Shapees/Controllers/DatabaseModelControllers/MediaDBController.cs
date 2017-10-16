using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.DatabaseModel;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Shapees.Controllers.DatabaseModelControllers
{
    public class MediaDBController : Controller
    {
        private readonly masterContext _context;

        public MediaDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: MediaDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Media.Include(m => m.Author).Include(m => m.Child);
            return View(await masterContext.ToListAsync());
        }

        // GET: MediaDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .Include(m => m.Author)
                .Include(m => m.Child)
                .SingleOrDefaultAsync(m => m.Mediaid == id);


            if (media == null)
            {
                return NotFound();
            }

            //if uploaded file path exists
            if (media.Filepath != null)
            {
                //load file and save it in viewbag
                var filename = media.Filepath;
                string path = FileStrem.GetFilePath("wwwroot/uploads/childportfolio/media/" + filename);
                byte[] imagebyte = LoadImage.GetPictureData(path);
                var base64 = Convert.ToBase64String(imagebyte);

                if (media.Mediatype == "image")
                {
                    //set viewbag image source
                    ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
                    //ViewBag.imagelegth = base64.Length;
                }

                if (media.Mediatype == "video")
                {
                    //set viewbag image source
                    ViewBag.videosrc = string.Format("data:image/png;base64,{0}", base64);
                }

            }

            return View(media);
        }

        // GET: MediaDB/Create
        public IActionResult Create()
        {
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "FullName");
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName");
            return View();
        }

        // POST: MediaDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mediaid,Mediatype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateuploaded,Title,Description,Filepath")] Media media, IFormFile file)
        {
            //get child info
            var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == media.Childid);
            //get autorinfo
            var authorinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == media.Authorid);

            //file handling
            if (file == null || file.Length == 0)
            {
                media.Filepath = null;
            }
            else
            {
                //set filename based on child information
                var filename = childdetails.Childfirstname.Trim() + childdetails.Childlastname.Trim() + childdetails.Childid.ToString() + file.FileName;
                //save file path
                media.Filepath = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/childportfolio/media",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            if (ModelState.IsValid)
            {
                //set author info
                media.Authorfirst = authorinfo.Firstname;
                media.Authorlast = authorinfo.Lastname;

                //set child info
                media.Childfirst = childdetails.Childfirstname;
                media.Childlast = childdetails.Childlastname;

                //set upload date
                media.Dateuploaded = DateTime.Today;

                _context.Add(media);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Userid", media.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childid", media.Childid);
            return View(media);
        }

        // GET: MediaDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media.SingleOrDefaultAsync(m => m.Mediaid == id);
            if (media == null)
            {
                return NotFound();
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", media.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", media.Childid);
            return View(media);
        }

        // POST: MediaDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mediaid,Mediatype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateuploaded,Title,Description,Filepath")] Media media)
        {
            if (id != media.Mediaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.Mediaid))
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
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", media.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", media.Childid);
            return View(media);
        }

        // GET: MediaDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .Include(m => m.Author)
                .Include(m => m.Child)
                .SingleOrDefaultAsync(m => m.Mediaid == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: MediaDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var media = await _context.Media.SingleOrDefaultAsync(m => m.Mediaid == id);
            _context.Media.Remove(media);

            var filename = media.Filepath;

            //check if filepath exists
            if (filename == string.Empty)
                return Content("File does not exist.");

            //get full file path
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/uploads/childportfolio/media/", filename);

            //if path exists, delete file
            if (path != null || path != string.Empty)
            {
                if ((System.IO.File.Exists(path)))
                {
                    System.IO.File.Delete(path);
                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Reference: https://www.codeproject.com/Articles/1203408/Upload-Download-Files-in-ASP-NET-Core
        //Function downloads file based on filename
        public async Task<IActionResult> Download(string filename)
        {
            //check if filename exists
            if (filename == null)
                return Content("File does not exist.");

            //get full file path
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/uploads/childportfolio/media/", filename);

            //open file in new memory stream
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            //get file extension
            var extIndex = filename.LastIndexOf('.');
            string ext = filename.Substring(extIndex, filename.Length - extIndex);
            ext = ext.Trim();

            //return file
            return File(memory, GetMimeType(filename, ext), filename + ext);
        }


        //Function returns mime type of the file
        private string GetMimeType(string fileName, string ext)
        {
            string mimeType = "application/unknown";

            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            return mimeType;
        }

        private bool MediaExists(int id)
        {
            return _context.Media.Any(e => e.Mediaid == id);
        }

        
    }
}
