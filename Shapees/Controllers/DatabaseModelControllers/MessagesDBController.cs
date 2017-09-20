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
    public class MessagesDBController : Controller
    {
        private readonly masterContext _context;

        public MessagesDBController(masterContext context)
        {
            _context = context;    
        }

        // GET: MessagesDB
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Message.Include(m => m.Author).Include(m => m.Receiver);
            return View(await masterContext.ToListAsync());
        }

        // GET: MessagesDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Author)
                .Include(m => m.Receiver)
                .SingleOrDefaultAsync(m => m.Messageid == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: MessagesDB/Create
        public IActionResult Create()
        {
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email");
            ViewData["Receiverid"] = new SelectList(_context.Userinfo, "Userid", "Email");
            return View();
        }

        // POST: MessagesDB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Messageid,Authorfirst,Authorlast,Authorid,Receiverfirst,Receiverlast,Receiverid,Datesent,Datereceived,Dateopened,Isopened,Isrepliedto,Subject,Description")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Authorid);
            ViewData["Receiverid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Receiverid);
            return View(message);
        }

        // GET: MessagesDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.Messageid == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Authorid);
            ViewData["Receiverid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Receiverid);
            return View(message);
        }

        // POST: MessagesDB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Messageid,Authorfirst,Authorlast,Authorid,Receiverfirst,Receiverlast,Receiverid,Datesent,Datereceived,Dateopened,Isopened,Isrepliedto,Subject,Description")] Message message)
        {
            if (id != message.Messageid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Messageid))
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
            ViewData["Authorid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Authorid);
            ViewData["Receiverid"] = new SelectList(_context.Userinfo, "Userid", "Email", message.Receiverid);
            return View(message);
        }

        // GET: MessagesDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Author)
                .Include(m => m.Receiver)
                .SingleOrDefaultAsync(m => m.Messageid == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: MessagesDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.SingleOrDefaultAsync(m => m.Messageid == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Messageid == id);
        }
    }
}
