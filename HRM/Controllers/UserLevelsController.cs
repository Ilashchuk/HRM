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
    public class UserLevelsController : Controller
    {
        private readonly HRMContext _context;

        public UserLevelsController(HRMContext context)
        {
            _context = context;
        }

        // GET: UserLevels
        public async Task<IActionResult> Index()
        {
              return _context.UserLevels != null ? 
                          View(await _context.UserLevels.ToListAsync()) :
                          Problem("Entity set 'HRMContext.UserLevels'  is null.");
        }

        // GET: UserLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserLevels == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLevel == null)
            {
                return NotFound();
            }

            return View(userLevel);
        }

        // GET: UserLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] UserLevel userLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userLevel);
        }

        // GET: UserLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserLevels == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels.FindAsync(id);
            if (userLevel == null)
            {
                return NotFound();
            }
            return View(userLevel);
        }

        // POST: UserLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UserLevel userLevel)
        {
            if (id != userLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLevelExists(userLevel.Id))
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
            return View(userLevel);
        }

        // GET: UserLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserLevels == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLevel == null)
            {
                return NotFound();
            }

            return View(userLevel);
        }

        // POST: UserLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserLevels == null)
            {
                return Problem("Entity set 'HRMContext.UserLevels'  is null.");
            }
            var userLevel = await _context.UserLevels.FindAsync(id);
            if (userLevel != null)
            {
                _context.UserLevels.Remove(userLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLevelExists(int id)
        {
          return (_context.UserLevels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
