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
using HRM.Services.StatusesServices;
using HRM.Services;

namespace HRM.Controllers
{
    [Authorize(Roles = "HR, TeamLead")]
    public class UsersController : Controller
    {
        private readonly IUsersControlService _usersCS;
        private readonly IStatusesControlService _statusesCS;
        private readonly IGenericControlService<Team> _teamsCS;
        private readonly IGenericControlService<UserLevel> _userLevelCS;
        private readonly IGenericControlService<RoleType> _roleTypeCS;

        public UsersController(IUsersControlService usersControleService, 
            IStatusesControlService statusesControlService,
            IGenericControlService<Team> teamsControlService,
            IGenericControlService<UserLevel> userLevelControlService,
            IGenericControlService<RoleType> roleTypeCS)
        {
            _usersCS = usersControleService;
            _statusesCS = statusesControlService;
            _teamsCS = teamsControlService;
            _userLevelCS = userLevelControlService;
            _roleTypeCS = roleTypeCS;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {

            return View(await _usersCS.GetUsersListForCurrentUserAsync(
                await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name)));

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _usersCS.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Create()
        {
            ViewData["Role"] = new SelectList(await _roleTypeCS.GetListAsync(), "Id", "Name");
            ViewData["Team"] = new SelectList(await _teamsCS.GetListAsync(), "Id", "Name");
            ViewData["Level"] = new SelectList(await _userLevelCS.GetListAsync(), "Id", "Name");
            ViewData["Status"] = new SelectList(await _statusesCS.GetByStatusTypeIdAsync(_statusesCS.GetIdWitValueUserStatus()), "Id", "Name");
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
                var currentUser = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
                user.CompanyId = currentUser.CompanyId;
                user.StartDate = DateTime.Now;
                user.Status = await _statusesCS.GetByIdAsync(user.UserStatusId);
                await _usersCS.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Role"] = new SelectList(await _roleTypeCS.GetListAsync(), "Id", "Name");
            ViewData["Team"] = new SelectList(await _teamsCS.GetListAsync(), "Id", "Name");
            ViewData["Level"] = new SelectList(await _userLevelCS.GetListAsync(), "Id", "Name");
            ViewData["Status"] = new SelectList(await _statusesCS.GetByStatusTypeIdAsync(_statusesCS.GetIdWitValueUserStatus()), "Id", "Name");
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null && _usersCS.NotEmpty())
            {
                var user = await _usersCS.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                ViewData["Role"] = new SelectList(await _roleTypeCS.GetListAsync(), "Id", "Name");
                ViewData["Team"] = new SelectList(await _teamsCS.GetListAsync(), "Id", "Name");
                ViewData["Level"] = new SelectList(await _userLevelCS.GetListAsync(), "Id", "Name");
                ViewData["Status"] = new SelectList(await _statusesCS.GetByStatusTypeIdAsync(_statusesCS.GetIdWitValueUserStatus()), "Id", "Name");
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
            var currentUser = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
            user.CompanyId = currentUser.CompanyId;
            user.Status = await _statusesCS.GetByIdAsync(user.UserStatusId);
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _usersCS.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_usersCS.Exists(user.Id))
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

            ViewData["Role"] = new SelectList(await _roleTypeCS.GetListAsync(), "Id", "Name");
            ViewData["Team"] = new SelectList(await _teamsCS.GetListAsync(), "Id", "Name");
            ViewData["Level"] = new SelectList(await _userLevelCS.GetListAsync(), "Id", "Name");
            ViewData["Status"] = new SelectList(await _statusesCS.GetByStatusTypeIdAsync(_statusesCS.GetIdWitValueUserStatus()), "Id", "Name");
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _usersCS.GetByIdAsync(id);
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
            if (!_usersCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Users'  is null.");
            }
            var user = await _usersCS.GetByIdAsync(id);

            if (user != null)
            {
                await _usersCS.DeleteAsync(user);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
