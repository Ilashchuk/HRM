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
    public class SettingsController : Controller
    {
        private readonly IGenericControlService<Setting> _settingsCS;

        public SettingsController(IGenericControlService<Setting> settingsCS)
        {
            _settingsCS = settingsCS;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
              return _settingsCS.NotEmpty() ? 
                          View(await _settingsCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.Settings'  is null.");
        }

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var setting = await _settingsCS.GetByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeekDays,VacationDays")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                await _settingsCS.AddAsync(setting);
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var setting = await _settingsCS.GetByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeekDays,VacationDays")] Setting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _settingsCS.UpdateAsync(setting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_settingsCS.Exists(setting.Id))
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
            return View(setting);
        }

        // GET: Settings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var setting = await _settingsCS.GetByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_settingsCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Settings'  is null.");
            }
            var setting = await _settingsCS.GetByIdAsync(id);
            if (setting != null)
            {
                await _settingsCS.DeleteAsync(setting);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
