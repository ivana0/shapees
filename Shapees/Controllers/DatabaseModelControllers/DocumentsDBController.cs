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
    public class DocumentsDBController : Controller
    {
        private readonly masterContext _context;

        public DocumentsDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: DocumentsDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Document.Include(d => d.Author).Include(d => d.Child);
            return View(await masterContext.ToListAsync());
        }

        // GET: DocumentsDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.Author)
                .Include(d => d.Child)
                .SingleOrDefaultAsync(m => m.Documentid == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: DocumentsDB/Create
        public IActionResult Create()
        {
            //get educators and directors list only for selection
            var authors = _context.Userinfo.Where(e => e.Usertype != 1);


            ViewData["Authorid"] = new SelectList(authors, "Userid", "FullName");
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName");
            return View();
        }

        // POST: DocumentsDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Documentid,Documenttype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateuploaded,Title,Description,Filepath")] Document document, IFormFile file)
        {
            //get child info
            var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == document.Childid);
            //get autorinfo
            var authorinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == document.Authorid);

            //file handling
            if (file == null || file.Length == 0)
            {
                document.Filepath = null;
            }
            else
            {
                //set filename based on child information
                var filename = childdetails.Childfirstname.Trim() + childdetails.Childlastname.Trim() + childdetails.Childid.ToString() + file.FileName;
                //save file path
                document.Filepath = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/childportfolio/documents",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            if (ModelState.IsValid)
            {
                //set author info
                document.Authorfirst = authorinfo.Firstname;
                document.Authorlast = authorinfo.Lastname;

                //set child info
                document.Childfirst = childdetails.Childfirstname;
                document.Childlast = childdetails.Childlastname;

                //set upload date
                document.Dateuploaded = DateTime.Today;

                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Userid", document.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childid", document.Childid);
            return View(document);
        }

        // GET: DocumentsDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document.SingleOrDefaultAsync(m => m.Documentid == id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", document.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", document.Childid);
            return View(document);
        }

        // POST: DocumentsDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Documentid,Documenttype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateuploaded,Title,Description,Filepath")] Document document)
        {
            if (id != document.Documentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Documentid))
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
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", document.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", document.Childid);
            return View(document);
        }

        // GET: DocumentsDB/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.Author)
                .Include(d => d.Child)
                .SingleOrDefaultAsync(m => m.Documentid == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: DocumentsDB/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Document.SingleOrDefaultAsync(m => m.Documentid == id);
            _context.Document.Remove(document);


            var filename = document.Filepath;

            //check if filepath exists
            if (filename == string.Empty)
                return Content("File does not exist.");

            //get full file path
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/uploads/childportfolio/documents/", filename);

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
                           "wwwroot/uploads/childportfolio/documents/", filename);

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


        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.Documentid == id);
        }
    }
}
