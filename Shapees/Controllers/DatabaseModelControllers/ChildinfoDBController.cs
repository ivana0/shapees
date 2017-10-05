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
    public class ChildinfoDBController : Controller
    {
        private readonly masterContext _context;

        public ChildinfoDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: ChildinfoDB
        public async Task<IActionResult> Index(string searchString, string searchchild, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;

            //var masterContext = _context.Childinfo.Include(c => c.Educator).Include(c => c.Parent1Navigation).Include(c => c.Parent2Navigation).Include(c => c.Room);

            var searchchildren = from c in _context.Childinfo.Include(c => c.Educator).Include(c => c.Parent1Navigation).Include(c => c.Parent2Navigation).Include(c => c.Room)
                                 select c;

            //search users
            if (!String.IsNullOrEmpty(searchString) && searchchild == "byfirstlast")
            {
                string[] words = searchString.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in words)
                {
                    searchchildren = searchchildren.Where(s => s.Childfirstname.Contains(str) || s.Childlastname.Contains(str));
                }
            }
            else if (!String.IsNullOrEmpty(searchString) && searchchild == "byroom")
            {
                searchchildren = searchchildren.Where(s => s.Inroom.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_asc":
                    searchchildren = searchchildren.OrderBy(s => s.Childfirstname);
                    break;
                case "surname_asc":
                    searchchildren = searchchildren.OrderBy(s => s.Childlastname);
                    break;
                default:
                    searchchildren = searchchildren.OrderBy(s => s.Childfirstname);
                    break;
            }

            return View(await searchchildren.ToListAsync());
        }

        // GET: ChildinfoDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childinfo = await _context.Childinfo
                .Include(c => c.Educator)
                .Include(c => c.Parent1Navigation)
                .Include(c => c.Parent2Navigation)
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.Childid == id);
            if (childinfo == null)
            {
                return NotFound();
            }

            return View(childinfo);
        }

        // GET: ChildinfoDB/Create
        public IActionResult Create()
        {
            ViewData["Educatorid"] = new SelectList(_context.Userinfo, "Userid", "Email");
            ViewData["Parent1"] = new SelectList(_context.Userinfo, "Userid", "Email");
            ViewData["Parent2"] = new SelectList(_context.Userinfo, "Userid", "Email");
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid");
            return View();
        }

        // POST: ChildinfoDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Childid,Inroom,Educatorfname,Educatorlname,Educatorid,Street,City,Postcode,State,Childfirstname,Childlastname,Dob,Currentage,Contacnumber1,Contacnumber2,Parent1,Parent1fname,Parent1lname,Parent2,Parent2fname,Parent2lname,Shortinfo,Specialneeds,Profileimage")] Childinfo childinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Educatorid"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Educatorid);
            ViewData["Parent1"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent1);
            ViewData["Parent2"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent2);
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", childinfo.Roomid);
            return View(childinfo);
        }

        // GET: ChildinfoDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childinfo = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == id);
            if (childinfo == null)
            {
                return NotFound();
            }
            ViewData["Educatorid"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Educatorid);
            ViewData["Parent1"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent1);
            ViewData["Parent2"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent2);
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", childinfo.Roomid);
            return View(childinfo);
        }

        // POST: ChildinfoDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Childid,Inroom,Educatorfname,Educatorlname,Educatorid,Street,City,Postcode,State,Childfirstname,Childlastname,Dob,Currentage,Contacnumber1,Contacnumber2,Parent1,Parent1fname,Parent1lname,Parent2,Parent2fname,Parent2lname,Shortinfo,Specialneeds,Profileimage")] Childinfo childinfo)
        {
            if (id != childinfo.Childid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildinfoExists(childinfo.Childid))
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
            ViewData["Educatorid"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Educatorid);
            ViewData["Parent1"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent1);
            ViewData["Parent2"] = new SelectList(_context.Userinfo, "Userid", "Email", childinfo.Parent2);
            ViewData["Roomid"] = new SelectList(_context.Room, "Roomid", "Roomid", childinfo.Roomid);
            return View(childinfo);
        }

        // GET: ChildinfoDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childinfo = await _context.Childinfo
                .Include(c => c.Educator)
                .Include(c => c.Parent1Navigation)
                .Include(c => c.Parent2Navigation)
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.Childid == id);
            if (childinfo == null)
            {
                return NotFound();
            }

            return View(childinfo);
        }

        // POST: ChildinfoDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var childinfo = await _context.Childinfo.SingleOrDefaultAsync(m => m.Childid == id);
            _context.Childinfo.Remove(childinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChildinfoExists(int id)
        {
            return _context.Childinfo.Any(e => e.Childid == id);
        }
    }
}
