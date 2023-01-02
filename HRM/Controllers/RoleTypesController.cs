using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services;

namespace HRM.Controllers
{
    public class RoleTypesController : Controller
    {
        private readonly IGenericControlService<RoleType> _roleTypeCS;

        public RoleTypesController(IGenericControlService<RoleType> roleTypeCS)
        {
            _roleTypeCS = roleTypeCS;
        }

        // GET: RoleTypes
        public async Task<IActionResult> Index()
        {
              return _roleTypeCS.NotEmpty() ? 
                          View(await _roleTypeCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.RoleTypes'  is null.");
        }

        // GET: RoleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var roleType = await _roleTypeCS.GetByIdAsync(id);
            if (roleType == null)
            {
                return NotFound();
            }

            return View(roleType);
        }

        // GET: RoleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RoleType roleType)
        {
            if (ModelState.IsValid)
            {
                await _roleTypeCS.AddAsync(roleType);
                return RedirectToAction(nameof(Index));
            }
            return View(roleType);
        }

        // GET: RoleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var roleType = await _roleTypeCS.GetByIdAsync(id);
            if (roleType == null)
            {
                return NotFound();
            }
            return View(roleType);
        }

        // POST: RoleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RoleType roleType)
        {
            if (id != roleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleTypeCS.UpdateAsync(roleType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_roleTypeCS.Exists(id))
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
            return View(roleType);
        }

        // GET: RoleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var roleType = await _roleTypeCS.GetByIdAsync(id);
            if (roleType == null)
            {
                return NotFound();
            }

            return View(roleType);
        }

        // POST: RoleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_roleTypeCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.RoleTypes'  is null.");
            }
            var roleType = await _roleTypeCS.GetByIdAsync(id);
            if (roleType != null)
            {
                await _roleTypeCS.DeleteAsync(roleType);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
