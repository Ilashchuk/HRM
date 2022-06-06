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
    public class UsersController : Controller
    {
        private readonly HRMContext _context;

        public UsersController(HRMContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var hRMContext = _context.Users.Include(u => u.Company).Include(u => u.RoleType).Include(u => u.Team).Include(u => u.UserLevel);
            return View(await hRMContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Company)
                .Include(u => u.RoleType)
                .Include(u => u.Team)
                .Include(u => u.UserLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["RoleTypeId"] = new SelectList(_context.RoleTypes, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["UserLevelId"] = new SelectList(_context.UserLevels, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Password,Email,StartDate,UserStatusId,UserLevelId,TeamId,RoleTypeId,CompanyId")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", user.CompanyId);
            ViewData["RoleTypeId"] = new SelectList(_context.RoleTypes, "Id", "Id", user.RoleTypeId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", user.TeamId);
            ViewData["UserLevelId"] = new SelectList(_context.UserLevels, "Id", "Id", user.UserLevelId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", user.CompanyId);
            ViewData["RoleTypeId"] = new SelectList(_context.RoleTypes, "Id", "Id", user.RoleTypeId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", user.TeamId);
            ViewData["UserLevelId"] = new SelectList(_context.UserLevels, "Id", "Id", user.UserLevelId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Password,Email,StartDate,UserStatusId,UserLevelId,TeamId,RoleTypeId,CompanyId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", user.CompanyId);
            ViewData["RoleTypeId"] = new SelectList(_context.RoleTypes, "Id", "Id", user.RoleTypeId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", user.TeamId);
            ViewData["UserLevelId"] = new SelectList(_context.UserLevels, "Id", "Id", user.UserLevelId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Company)
                .Include(u => u.RoleType)
                .Include(u => u.Team)
                .Include(u => u.UserLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'HRMContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
