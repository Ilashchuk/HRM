using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services.StatusesServices;
using HRM.Services.StatusTypesServices;

namespace HRM.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusesControlService _statusesControlService;
        private readonly IStatusTypesControlService _statusTypesControlService;
        public StatusController(IStatusesControlService statusesControlService, IStatusTypesControlService statusTypesControlService)
        {
            _statusesControlService = statusesControlService;
            _statusTypesControlService = statusTypesControlService;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            return View(await _statusesControlService.GetStatusesAsync());
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var status = await _statusesControlService.GetStatusByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StatusType"] = new SelectList(await _statusTypesControlService.GetStatusTypesAsync(), "Id", "Name");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusTypeId,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                await _statusesControlService.AddStatusAsync(status);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusType"] = new SelectList(await _statusTypesControlService.GetStatusTypesAsync(), "Id", "Name");
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var status = await _statusesControlService.GetStatusByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }
            ViewData["StatusType"] = new SelectList(await _statusTypesControlService.GetStatusTypesAsync(), "Id", "Name");
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusTypeId,Name")] Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _statusesControlService.UpdateStatusAsync(status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_statusesControlService.StatusExists(status.Id))
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
            ViewData["StatusTypeId"] = new SelectList(await _statusTypesControlService.GetStatusTypesAsync(), "Id", "Name");
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var status = await _statusesControlService.GetStatusByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_statusesControlService.StatusesTableIsNotEmpty())
            {
                return Problem("Entity set 'HRMContext.Statuses'  is null.");
            }
            var status = await _statusesControlService.GetStatusByIdAsync(id);
            if (status != null)
            {
                await _statusesControlService.DeleteStatusAsync(status);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
