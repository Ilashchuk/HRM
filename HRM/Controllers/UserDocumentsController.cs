using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;

namespace HRM.Controllers
{
    public class UserDocumentsController : Controller
    {
        private readonly HRMContext _context;

        public UserDocumentsController(HRMContext context)
        {
            _context = context;
        }

        // GET: UserDocuments
        public async Task<IActionResult> Index()
        {
            var hRMContext = _context.UserDocuments.Include(u => u.User);
            return View(await hRMContext.ToListAsync());
        }

        // GET: UserDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserDocuments == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDocument == null)
            {
                return NotFound();
            }

            return View(userDocument);
        }

        // GET: UserDocuments/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,DocumentLinck")] UserDocument userDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.UserId);
            return View(userDocument);
        }

        // GET: UserDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserDocuments == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments.FindAsync(id);
            if (userDocument == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.UserId);
            return View(userDocument);
        }

        // POST: UserDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DocumentLinck")] UserDocument userDocument)
        {
            if (id != userDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDocumentExists(userDocument.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.UserId);
            return View(userDocument);
        }

        // GET: UserDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserDocuments == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDocument == null)
            {
                return NotFound();
            }

            return View(userDocument);
        }

        // POST: UserDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserDocuments == null)
            {
                return Problem("Entity set 'HRMContext.UserDocuments'  is null.");
            }
            var userDocument = await _context.UserDocuments.FindAsync(id);
            if (userDocument != null)
            {
                _context.UserDocuments.Remove(userDocument);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDocumentExists(int id)
        {
          return (_context.UserDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
