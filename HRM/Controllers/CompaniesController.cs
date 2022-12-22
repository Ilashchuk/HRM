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
using HRM.Services;

namespace HRM.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IGenericControlService<Company> _companiesCS;

        public CompaniesController(IGenericControlService<Company> companiesControlService)
        {
            _companiesCS = companiesControlService;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
              return _companiesCS.NotEmpty() ? 
                          View(await _companiesCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.Companies'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var company = await _companiesCS.GetByIdAsync(id);
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
                await _companiesCS.AddAsync(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var company = await _companiesCS.GetByIdAsync(id);
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
                    await _companiesCS.UpdateAsync(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_companiesCS.Exists(id))
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
            var company = await _companiesCS.GetByIdAsync(id);
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
            if (!_companiesCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Companies'  is null.");
            }
            var company = await _companiesCS.GetByIdAsync(id);
            if (company != null)
            {
                await _companiesCS.DeleteAsync(company);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
