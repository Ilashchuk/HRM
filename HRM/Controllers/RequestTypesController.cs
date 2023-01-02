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
    public class RequestTypesController : Controller
    {
        private readonly IGenericControlService<RequestType> _requestTypeCS;

        public RequestTypesController(IGenericControlService<RequestType> requestTypeCS)
        {
            _requestTypeCS = requestTypeCS;
        }

        // GET: RequestTypes
        public async Task<IActionResult> Index()
        {
              return _requestTypeCS.NotEmpty() ? 
                          View(await _requestTypeCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.RequestTypes'  is null.");
        }

        // GET: RequestTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var requestType = await _requestTypeCS.GetByIdAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }

            return View(requestType);
        }

        // GET: RequestTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RequestTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                await _requestTypeCS.AddAsync(requestType);
                return RedirectToAction(nameof(Index));
            }
            return View(requestType);
        }

        // GET: RequestTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var requestType = await _requestTypeCS.GetByIdAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RequestType requestType)
        {
            if (id != requestType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _requestTypeCS.UpdateAsync(requestType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_requestTypeCS.Exists(id))
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
            return View(requestType);
        }

        // GET: RequestTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var requestType = await _requestTypeCS.GetByIdAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }

            return View(requestType);
        }

        // POST: RequestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_requestTypeCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.RequestTypes'  is null.");
            }
            var requestType = await _requestTypeCS.GetByIdAsync(id);
            if (requestType != null)
            {
                await _requestTypeCS.DeleteAsync(requestType);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
