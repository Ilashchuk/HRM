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
using HRM.Services;

namespace HRM.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusesControlService _statusesCS;
        private readonly IGenericControlService<StatusType> _statusTypesCS;
        public StatusController(IStatusesControlService statusesControlService, IGenericControlService<StatusType> statusTypesControlService)
        {
            _statusesCS = statusesControlService;
            _statusTypesCS = statusTypesControlService;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            return View(await _statusesCS.GetListAsync());
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var status = await _statusesCS.GetByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StatusType"] = new SelectList(await _statusTypesCS.GetListAsync(), "Id", "Name");
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
                await _statusesCS.AddAsync(status);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusType"] = new SelectList(await _statusTypesCS.GetListAsync(), "Id", "Name");
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var status = await _statusesCS.GetByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }
            ViewData["StatusType"] = new SelectList(await _statusTypesCS.GetListAsync(), "Id", "Name");
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
                    await _statusesCS.UpdateAsync(status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_statusesCS.Exists(status.Id))
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
            ViewData["StatusTypeId"] = new SelectList(await _statusTypesCS.GetListAsync(), "Id", "Name");
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var status = await _statusesCS.GetByIdAsync(id);

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
            if (!_statusesCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Statuses'  is null.");
            }
            var status = await _statusesCS.GetByIdAsync(id);
            if (status != null)
            {
                await _statusesCS.DeleteAsync(status);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
