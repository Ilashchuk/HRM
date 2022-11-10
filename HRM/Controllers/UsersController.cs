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
using HRM.Services.UsersServices;

namespace HRM.Controllers
{
    [Authorize(Roles = "HR, TeamLead")]
    public class UsersController : Controller
    {
        //private readonly HRMContext _context;
        private readonly IUsersControlService _usersControleService;

        public UsersController(IUsersControlService usersControleService, HRMContext context)
        {
            _usersControleService = usersControleService;
            //_context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {

            return View(await _usersControleService.GetUsersListForCurrentUserAsync(
                await _usersControleService.GetUserByEmailAsync(HttpContext.User.Identity.Name)));

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _usersControleService.GetUserByIdAsync(id);

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
                var currentUser = await _usersControleService.GetUserByEmailAsync(HttpContext.User.Identity.Name);
                user.CompanyId = currentUser.CompanyId;
                user.StartDate = DateTime.Now;
                user.Status = _context.Statuses.First(st => st.Id == user.UserStatusId);
                await _usersControleService.AddUserAsync(user);
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
            if (id != null && _usersControleService.UsersTableIsNotEmpty())
            {
                var user = await _usersControleService.GetUserByIdAsync(id);
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
            return NotFound();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,StartDate,UserStatusId,UserLevelId,TeamId,RoleTypeId,CompanyId")] User user)
        {
            var currentUser = await _usersControleService.GetUserByEmailAsync(HttpContext.User.Identity.Name);
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
                    await _usersControleService.UpdateUserAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_usersControleService.UserExists(user.Id))
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
            var user = await _usersControleService.GetUserByIdAsync(id);
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
            if (!_usersControleService.UsersTableIsNotEmpty())
            {
                return Problem("Entity set 'HRMContext.Users'  is null.");
            }
            var user = await _usersControleService.GetUserByIdAsync(id);

            if (user != null)
            {
                await _usersControleService.DeleteUserAsync(user);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
