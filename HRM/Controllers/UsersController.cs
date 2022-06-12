using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using HRM.Services;

namespace HRM.Controllers
{
    [Authorize(Roles = "HR, TeamLead")]
    public class UsersController : Controller
    {
        private readonly HRMContext _context;
        private UsersControlService _usersControleService;

        public UsersController(HRMContext context)
        {
            _usersControleService = new UsersControlService(context);
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            User user = _usersControleService.GetUser(HttpContext.User.Identity.Name);
            //await _context.Users.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);
            RoleType HR = _usersControleService.GetRole("HR");
                          //await _context.RoleTypes.FirstOrDefaultAsync(r => r.Name == "HR");

            var hRMContext = _context.Users.Include(u => u.Company).Include(u => u.RoleType)
                    .Include(u => u.Team).Include(u => u.UserLevel).Include(u => u.Status)
                    .Where(u => u.Id != user.Id && u.RoleTypeId != HR.Id);

            if (HttpContext.User.IsInRole("TeamLead"))
                return View(await hRMContext.Where(u => u.TeamId == user.TeamId).ToListAsync());
            
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
                .Include(u => u.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "HR")]
        public IActionResult Create()
        {

            ViewData["Role"] = new SelectList(_context.RoleTypes, "Id", "Name");
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["Level"] = new SelectList(_context.UserLevels, "Id", "Name");
            ViewData["Status"] = new SelectList(_context.Statuses.Where(s => s.StatusTypeId == _usersControleService.GetUserStatusId()), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Create([Bind("Id,FullName,Password,Email,StartDate,UserStatusId,UserLevelId,TeamId,RoleTypeId,CompanyId")] User user)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _usersControleService.GetUser(HttpContext.User.Identity.Name);
                user.CompanyId = currentUser.CompanyId;
                user.StartDate = DateTime.Now;
                user.Status = _context.Statuses.First(st => st.Id == user.UserStatusId);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Role"] = new SelectList(_context.RoleTypes, "Id", "Name");
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["Level"] = new SelectList(_context.UserLevels, "Id", "Name");
            ViewData["Status"] = new SelectList(_context.Statuses.Where(s => s.StatusTypeId == _usersControleService.GetUserStatusId()), "Id", "Name");
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "HR")]
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

            ViewData["Role"] = new SelectList(_context.RoleTypes, "Id", "Name");
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["Level"] = new SelectList(_context.UserLevels, "Id", "Name");
            ViewData["Status"] = new SelectList(_context.Statuses.Where(s => s.StatusTypeId == _usersControleService.GetUserStatusId()), "Id", "Name");
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,StartDate,UserStatusId,UserLevelId,TeamId,RoleTypeId,CompanyId")] User user)
        {
            User currentUser = _usersControleService.GetUser(HttpContext.User.Identity.Name);
            user.CompanyId = currentUser.CompanyId;
            user.Status = _context.Statuses.First(st => st.Id == user.UserStatusId);
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

            ViewData["Role"] = new SelectList(_context.RoleTypes, "Id", "Name");
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["Level"] = new SelectList(_context.UserLevels, "Id", "Name");
            ViewData["Status"] = new SelectList(_context.Statuses.Where(s => s.StatusTypeId == _usersControleService.GetUserStatusId()), "Id", "Name");
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "HR")]
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
                .Include(u => u.Status)
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
        [Authorize(Roles = "HR")]
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
