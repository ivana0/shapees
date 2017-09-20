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
    public class AnnouncementsDBController : Controller
    {
        private readonly masterContext _context;

        public AnnouncementsDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: AnnouncementsDB
        public async Task<IActionResult> Index()
        {
            return View(await _context.Announcement.ToListAsync());
        }

        // GET: AnnouncementsDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .SingleOrDefaultAsync(m => m.Announcementid == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: AnnouncementsDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnnouncementsDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Announcementid,Announcementtype,Datecreated,Title,Description,Isdisplayed,Announcementfor")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: AnnouncementsDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Announcementid == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: AnnouncementsDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Announcementid,Announcementtype,Datecreated,Title,Description,Isdisplayed,Announcementfor")] Announcement announcement)
        {
            if (id != announcement.Announcementid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Announcementid))
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
            return View(announcement);
        }

        // GET: AnnouncementsDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .SingleOrDefaultAsync(m => m.Announcementid == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: AnnouncementsDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Announcementid == id);
            _context.Announcement.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcement.Any(e => e.Announcementid == id);
        }
    }
}
