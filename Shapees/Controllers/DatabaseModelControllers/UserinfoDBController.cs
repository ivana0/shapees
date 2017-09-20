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
            return View(await _context.Userinfo.ToListAsync());
        }

        // GET: UserinfoDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                .SingleOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // GET: UserinfoDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserinfoDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Email,Pass,Usertype,Lastlogin,Isloggedin,Street,City,Postcode,State,Firstname,Lastname,Dob,Homephone,Mobilephone,Employedon,Roomassigned,Shortbio,Taskscompleted,Totaltasks,Othercontact,Parentof,Profileimage")] Userinfo userinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
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
    }
}
