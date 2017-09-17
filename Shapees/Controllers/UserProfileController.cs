using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.TestModels;

namespace Shapees.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly masterContext _context;

        public UserProfileController(masterContext context)
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

            return View(userprofile);
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
        public async Task<IActionResult> Create([Bind("ProfileId,FirstName,LastName,Dob,HomePhone,MobilePhone,AddressId,Userid,Profileimage")] Userprofile userprofile)
        {
            if (ModelState.IsValid)
            {
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
    }
}
