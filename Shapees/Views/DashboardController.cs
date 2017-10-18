using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.DatabaseModel;

namespace Shapees.Views
{
    public class DashboardController : Controller
    {
        private readonly masterContext _context;

        public DashboardController(masterContext context)
        {
            _context = context;    
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Userinfo.Include(u => u.Room);
            return View(await masterContext.ToListAsync());
        }

        // GET: Dashboard/Details/5
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

            return View(userinfo);
        }

        // GET: Dashboard/Create
        public IActionResult Create()
        {
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Description");
            return View();
        }

        // POST: Dashboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Email,Pass,Usertype,Usertypename,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Roomid,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Description", userinfo.Roomid);
            return View(userinfo);
        }

        // GET: Dashboard/Edit/5
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
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Description", userinfo.Roomid);
            return View(userinfo);
        }

        // POST: Dashboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Username,Email,Pass,Usertype,Usertypename,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Roomid,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo)
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
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Description", userinfo.Roomid);
            return View(userinfo);
        }

        // GET: Dashboard/Delete/5
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

        // POST: Dashboard/Delete/5
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
    }
}
