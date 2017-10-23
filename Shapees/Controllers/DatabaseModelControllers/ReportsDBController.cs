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
using System.Data.SqlTypes;

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
            //get educators list for assign educator select list
            var assignededucators = _context.Userinfo.Where(e => e.Usertype == 2);

            ViewData["Authorid"] = new SelectList(assignededucators, "Userid", "FullName");
             
            //ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "FullName");
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

            //get child info
            var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == report.Childid);
            //get autorinfo
            var authorinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == report.Authorid);

            //file handling
            if (file == null || file.Length == 0)
            {
                report.Attachmentpaths = null;
            }
            else
            {
                //set filename based on child information
                var filename = childdetails.Childfirstname.Trim() + childdetails.Childlastname.Trim() + childdetails.Childid.ToString() + file.FileName;
                //save file path
                report.Attachmentpaths = filename;
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads/reports/attachment",
                        filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                report.Attachmentpaths = path;
            }

            if (ModelState.IsValid)
            {
                /*      TRIM STRINGS    */
                //set author info
                report.Authorfirst = authorinfo.Firstname.Trim();
                report.Authorlast = authorinfo.Lastname.Trim();

                //set child info
                report.Childfirst = childdetails.Childfirstname.Trim();
                report.Childlast = childdetails.Childlastname.Trim();

                //set child info
                report.Childfirst = report.Childfirst.Trim();
                report.Childlast = report.Childlast.Trim();

                //set child info
                report.Authorfirst = report.Authorfirst.Trim();
                report.Authorfirst = report.Authorfirst.Trim();

                //set created and modified date
                report.Datecreated = DateTime.Today;
                report.Lastmodified = DateTime.Today;

                //is submitted or is completed: -1 for false, 1 for true
                //change is submitted date
                if (report.Issubmitted == 1)
                    report.Datesubmitted = DateTime.Today;
                else
                    report.Datesubmitted = null;

                //change is completed date
                if (report.Iscompleted == 1)
                    report.Datecompleted = DateTime.Today;
                else
                    report.Datecompleted = null;


                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Userid", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "Userid", report.Childid);
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

            //get child info
            var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == report.Childid);
            //get autorinfo
            var authorinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == report.Authorid);

            ViewData["room"] = authorinfo.Roomassigned.ToString();
            ViewData["shortbio"] = authorinfo.Shortbio.ToString();
            ViewData["childdob"] = childdetails.Dob.ToString("dd/MM/yyyy");
            //ViewBag.roomassgined = authorinfo.Roomassigned.ToString();

            //get tasks
            var tasksinfo = await _context.Task.SingleOrDefaultAsync(m => m.Taskid == report.Taskid);

            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "FullName", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName", report.Childid);
            ViewData["Taskid"] = new SelectList(_context.Task, "Taskid", "Taskid");
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

                //get child info
                var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == report.Childid);
                //get autorinfo
                var authorinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Userid == report.Authorid);

                /*      TRIM STRINGS    */
                //set author info
                report.Authorfirst = authorinfo.Firstname.Trim();
                report.Authorlast = authorinfo.Lastname.Trim();

                //set child info
                report.Childfirst = childdetails.Childfirstname.Trim();
                report.Childlast = childdetails.Childlastname.Trim();

                //set child info
                report.Childfirst = report.Childfirst.Trim();
                report.Childlast = report.Childlast.Trim();

                //set child info
                report.Authorfirst = report.Authorfirst.Trim();
                report.Authorfirst = report.Authorfirst.Trim();

                ViewData["room"] = authorinfo.Roomassigned.ToString();
                ViewData["shortbio"] = authorinfo.Shortbio.ToString();
                ViewData["childdob"] = childdetails.Dob.ToString("dd/MM/yyyy");

                //change last modified if edited
                report.Lastmodified = DateTime.Today;

                //change is submitted date
                if (report.Issubmitted == 1)
                    report.Datesubmitted = DateTime.Today;
                else
                    report.Datesubmitted = null;

                //change is completed date
                if (report.Iscompleted == 1)
                    report.Datecompleted = DateTime.Today;
                else
                    report.Datecompleted = null;

                //report.Duedate = DateTime.Today;
                //Convert.ToDateTime(report.Duedate.ToString());

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
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "FullName", report.Authorid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName", report.Childid);
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
