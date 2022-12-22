using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services.UserLevelsServices;
using HRM.Services;

namespace HRM.Controllers
{
    public class UserLevelsController : Controller
    {
        private readonly IGenericControlService<UserLevel> _userLevelCS;
        public UserLevelsController(IGenericControlService<UserLevel> userLevelControlService)
        {
            _userLevelCS = userLevelControlService;
        }

        // GET: UserLevels
        public async Task<IActionResult> Index()
        {
              return _userLevelCS.NotEmpty() ? 
                          View(await _userLevelCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.UserLevels'  is null.");
        }

        // GET: UserLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userLevel = await _userLevelCS.GetByIdAsync(id);
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
                await _userLevelCS.AddAsync(userLevel);
                return RedirectToAction(nameof(Index));
            }
            return View(userLevel);
        }

        // GET: UserLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userLevel = await _userLevelCS.GetByIdAsync(id);
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
                    await _userLevelCS.UpdateAsync(userLevel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userLevelCS.Exists(userLevel.Id))
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
            var userLevel = await _userLevelCS.GetByIdAsync(id);
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
            if (!_userLevelCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.UserLevels'  is null.");
            }
            var userLevel = await _userLevelCS.GetByIdAsync(id);
            if (userLevel != null)
            {
                await _userLevelCS.DeleteAsync(userLevel);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
