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
    public class StatusTypesController : Controller
    {
        private readonly IGenericControlService<StatusType> _statusTypesCS;

        public StatusTypesController(IGenericControlService<StatusType> statusTypesControlService)
        {
            _statusTypesCS = statusTypesControlService;
        }

        // GET: StatusTypes
        public async Task<IActionResult> Index()
        {
              return View(await _statusTypesCS.GetListAsync());
        }

        // GET: StatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var statusType = await _statusTypesCS.GetByIdAsync(id);

            if (statusType == null)
            {
                return NotFound();
            }

            return View(statusType);
        }

        // GET: StatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StatusType statusType)
        {
            if (ModelState.IsValid)
            {
                await _statusTypesCS.AddAsync(statusType);
                return RedirectToAction(nameof(Index));
            }
            return View(statusType);
        }

        // GET: StatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var statusType = await _statusTypesCS.GetByIdAsync(id);

            if (statusType == null)
            {
                return NotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatusType statusType)
        {
            if (id != statusType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _statusTypesCS.UpdateAsync(statusType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_statusTypesCS.Exists(id))
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
            return View(statusType);
        }

        // GET: StatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var statusType = await _statusTypesCS.GetByIdAsync(id);

            if (statusType == null)
            {
                return NotFound();
            }

            return View(statusType);
        }

        // POST: StatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_statusTypesCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.StatusTypes'  is null.");
            }
            var statusType = await _statusTypesCS.GetByIdAsync(id);
            if (statusType != null)
            {
                await _statusTypesCS.DeleteAsync(statusType);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
