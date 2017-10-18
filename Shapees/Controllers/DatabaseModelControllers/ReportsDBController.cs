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
    public class ReportsDBController : Controller
    {
        private readonly masterContext _context;

        public ReportsDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: ReportsDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Report.Include(r => r.Author).Include(r => r.Child);
            return View(await masterContext.ToListAsync());
        }

        // GET: ReportsDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Author)
                .Include(r => r.Child)
                .SingleOrDefaultAsync(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: ReportsDB/Create
        public IActionResult Create()
        {
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "FullName");
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName");
            ViewData["Taskid"] = new SelectList(_context.Task, "Taskid", "Taskid");
            return View();
        }

        // POST: ReportsDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reportid,Reporttype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Datecreated,Lastmodified,Title,Subject,Bodytext,Filepath,Datesubmitted,Datecompleted,Issubmitted,Iscompleted,Taskid,Duedate,Attachmentpaths,Attachmentcount")] Report report, IFormFile file)
        {

            //file handling
            if (file == null || file.Length == 0)
            {
                report.Attachmentpaths = null;
            }
            else
            {
                //set filename
                var filename = file.FileName;
                //save file path
                report.Attachmentpaths = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/reports/attachment",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Userid", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childid", report.Childid);
            return View(report);
        }

        // GET: ReportsDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.SingleOrDefaultAsync(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", report.Childid);
            return View(report);
        }

        // POST: ReportsDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Reportid,Reporttype,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Datecreated,Lastmodified,Title,Subject,Bodytext,Filepath,Datesubmitted,Datecompleted,Issubmitted,Iscompleted,Taskid,Duedate,Attachmentpaths,Attachmentcount")] Report report)
        {
            if (id != report.Reportid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Reportid))
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
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Childfirstname", report.Childid);
            return View(report);
        }

        // GET: ReportsDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Author)
                .Include(r => r.Child)
                .SingleOrDefaultAsync(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: ReportsDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.SingleOrDefaultAsync(m => m.Reportid == id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Reportid == id);
        }
    }
}
