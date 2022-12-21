using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services.CompaniesServices;

namespace HRM.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompaniesControlService _companiesControlService;

        public CompaniesController(ICompaniesControlService companiesControlService)
        {
            _companiesControlService = companiesControlService;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
              return _companiesControlService.NotEmpty() ? 
                          View(await _companiesControlService.GetListAsync()) :
                          Problem("Entity set 'HRMContext.Companies'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var company = await _companiesControlService.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                await _companiesControlService.AddAsync(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var company = await _companiesControlService.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _companiesControlService.UpdateAsync(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_companiesControlService.Exists(id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var company = await _companiesControlService.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_companiesControlService.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Companies'  is null.");
            }
            var company = await _companiesControlService.GetByIdAsync(id);
            if (company != null)
            {
                await _companiesControlService.DeleteAsync(company);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
