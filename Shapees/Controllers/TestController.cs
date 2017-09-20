using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shapees.Models.TestModels;

namespace Shapees.TestModels
{
    public class TestController : Controller
    {
        private readonly testMasterContext _context;

        public TestController(testMasterContext context)
        {
            _context = context;    
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Userid == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Email,Pass,UserType,LastLogin,IsLoggedIn,ProfileId")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Userid == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Username,Email,Pass,UserType,LastLogin,IsLoggedIn,ProfileId")] Users users)
        {
            if (id != users.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Userid))
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
            return View(users);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Userid == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.Userid == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Userid == id);
        }
    }
}
