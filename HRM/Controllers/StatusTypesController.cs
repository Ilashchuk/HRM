using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services.StatusTypesServices;

namespace HRM.Controllers
{
    public class StatusTypesController : Controller
    {
        private readonly IStatusTypesControlService _statusTypesControlService;

        public StatusTypesController(IStatusTypesControlService statusTypesControlService)
        {
            _statusTypesControlService = statusTypesControlService;
        }

        // GET: StatusTypes
        public async Task<IActionResult> Index()
        {
              return View(await _statusTypesControlService.GetStatusTypesAsync());
        }

        // GET: StatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var statusType = await _statusTypesControlService.GetStatusTypeByIdAsync(id);

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
                await _statusTypesControlService.AddStatusTypeAsync(statusType);
                return RedirectToAction(nameof(Index));
            }
            return View(statusType);
        }

        // GET: StatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var statusType = await _statusTypesControlService.GetStatusTypeByIdAsync(id);

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
                    await _statusTypesControlService.UpdateStatusTypeAsync(statusType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_statusTypesControlService.StatusTypeExists(id))
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
            var statusType = await _statusTypesControlService.GetStatusTypeByIdAsync(id);

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
            if (!_statusTypesControlService.StatusTypesTableIsNotEmpty())
            {
                return Problem("Entity set 'HRMContext.StatusTypes'  is null.");
            }
            var statusType = await _statusTypesControlService.GetStatusTypeByIdAsync(id);
            if (statusType != null)
            {
                await _statusTypesControlService.DeleteStatusTypeAsync(statusType);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
