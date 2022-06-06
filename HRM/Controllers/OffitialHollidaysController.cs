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
    public class OffitialHollidaysController : Controller
    {
        private readonly HRMContext _context;

        public OffitialHollidaysController(HRMContext context)
        {
            _context = context;
        }

        // GET: OffitialHollidays
        public async Task<IActionResult> Index()
        {
              return _context.OffitialHollidays != null ? 
                          View(await _context.OffitialHollidays.ToListAsync()) :
                          Problem("Entity set 'HRMContext.OffitialHollidays'  is null.");
        }

        // GET: OffitialHollidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OffitialHollidays == null)
            {
                return NotFound();
            }

            var offitialHolliday = await _context.OffitialHollidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offitialHolliday == null)
            {
                return NotFound();
            }

            return View(offitialHolliday);
        }

        // GET: OffitialHollidays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OffitialHollidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date")] OffitialHolliday offitialHolliday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offitialHolliday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offitialHolliday);
        }

        // GET: OffitialHollidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OffitialHollidays == null)
            {
                return NotFound();
            }

            var offitialHolliday = await _context.OffitialHollidays.FindAsync(id);
            if (offitialHolliday == null)
            {
                return NotFound();
            }
            return View(offitialHolliday);
        }

        // POST: OffitialHollidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] OffitialHolliday offitialHolliday)
        {
            if (id != offitialHolliday.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offitialHolliday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffitialHollidayExists(offitialHolliday.Id))
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
            return View(offitialHolliday);
        }

        // GET: OffitialHollidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OffitialHollidays == null)
            {
                return NotFound();
            }

            var offitialHolliday = await _context.OffitialHollidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offitialHolliday == null)
            {
                return NotFound();
            }

            return View(offitialHolliday);
        }

        // POST: OffitialHollidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OffitialHollidays == null)
            {
                return Problem("Entity set 'HRMContext.OffitialHollidays'  is null.");
            }
            var offitialHolliday = await _context.OffitialHollidays.FindAsync(id);
            if (offitialHolliday != null)
            {
                _context.OffitialHollidays.Remove(offitialHolliday);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffitialHollidayExists(int id)
        {
          return (_context.OffitialHollidays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
