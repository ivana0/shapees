using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.DatabaseModel;

namespace Shapees.Controllers.DatabaseModelControllers
{
    public class TasksDBController : Controller
    {
        private readonly masterContext _context;

        public TasksDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: TasksDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Task.Include(t => t.Assignedfor).Include(t => t.Child).Include(t => t.Report);
            return View(await masterContext.ToListAsync());
        }

        // GET: TasksDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Assignedfor)
                .Include(t => t.Child)
                .Include(t => t.Report)
                .SingleOrDefaultAsync(m => m.Taskid == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: TasksDB/Create
        public IActionResult Create()
        {
            ViewData["Assignedforid"] = new SelectList(_context.Userinfo, "Userid", "FullName");
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName");
            ViewData["Reportid"] = new SelectList(_context.Report, "Reportid", "AuthorFullName");
            return View();
        }

        // POST: TasksDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Taskid,Taskforeducator,Taskforchild,Assignedforid,Taskforfirst,Taskforlast,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateassigned,Datecompleted,Duedate,Reportid,Reportpath,Issubmitted,Iscompleted")] Models.DatabaseModel.Task task)
        {
            //get child info
            var childdetails = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == task.Childid);

            if (ModelState.IsValid)
            {
                task.Dateassigned = DateTime.Today;
                   
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Assignedforid"] = new SelectList(_context.Userinfo, "Userid", "FullName", task.Assignedforid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName", task.Childid);
            ViewData["Reportid"] = new SelectList(_context.Report, "Reportid", "AuthorFullName", task.Reportid);
            return View(task);
        }

        // GET: TasksDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.SingleOrDefaultAsync(m => m.Taskid == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["Assignedforid"] = new SelectList(_context.Userinfo, "Userid", "Fullname", task.Assignedforid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName", task.Childid);
            ViewData["Reportid"] = new SelectList(_context.Report, "Reportid", "AuthorFullName", task.Reportid);
            return View(task);
        }

        // POST: TasksDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Taskid,Taskforeducator,Taskforchild,Assignedforid,Taskforfirst,Taskforlast,Authorfirst,Authorlast,Authorid,Childid,Childfirst,Childlast,Dateassigned,Datecompleted,Duedate,Reportid,Reportpath,Issubmitted,Iscompleted")] Models.DatabaseModel.Task task)
        {
            if (id != task.Taskid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Taskid))
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
            ViewData["Assignedforid"] = new SelectList(_context.Userinfo, "Userid", "FullName", task.Assignedforid);
            ViewData["Childid"] = new SelectList(_context.Childinfo, "Childid", "FullName", task.Childid);
            ViewData["Reportid"] = new SelectList(_context.Report, "Reportid", "AuthorFullName", task.Reportid);
            return View(task);
        }

        // GET: TasksDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Assignedfor)
                .Include(t => t.Child)
                .Include(t => t.Report)
                .SingleOrDefaultAsync(m => m.Taskid == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: TasksDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.SingleOrDefaultAsync(m => m.Taskid == id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Taskid == id);
        }
    }
}
