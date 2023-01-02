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
    public class OffitialHollidaysController : Controller
    {
        private readonly IGenericControlService<OffitialHolliday> _offitialHollidayCS;

        public OffitialHollidaysController(IGenericControlService<OffitialHolliday> offitialHollidayCS)
        {
            _offitialHollidayCS = offitialHollidayCS;
        }

        // GET: OffitialHollidays
        public async Task<IActionResult> Index()
        {
              return _offitialHollidayCS.NotEmpty() ? 
                          View(await _offitialHollidayCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.OffitialHollidays'  is null.");
        }

        // GET: OffitialHollidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var offitialHolliday = await _offitialHollidayCS.GetByIdAsync(id);
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
                await _offitialHollidayCS.AddAsync(offitialHolliday);
                return RedirectToAction(nameof(Index));
            }
            return View(offitialHolliday);
        }

        // GET: OffitialHollidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var offitialHolliday = await _offitialHollidayCS.GetByIdAsync(id);
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
                    await _offitialHollidayCS.UpdateAsync(offitialHolliday);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_offitialHollidayCS.Exists(offitialHolliday.Id))
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
            var offitialHolliday = await _offitialHollidayCS.GetByIdAsync(id);
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
            if (!_offitialHollidayCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.OffitialHollidays'  is null.");
            }
            var offitialHolliday = await _offitialHollidayCS.GetByIdAsync(id);
            if (offitialHolliday != null)
            {
                await _offitialHollidayCS.DeleteAsync(offitialHolliday);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
