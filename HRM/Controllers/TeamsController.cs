using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Data;
using HRM.Models;
using HRM.Services.TeamsServices;
using HRM.Services;

namespace HRM.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IGenericControlService<Team> _teamsCS;

        public TeamsController(IGenericControlService<Team> teamsControlService)
        {
            _teamsCS = teamsControlService;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
              return _teamsCS.NotEmpty() ? 
                          View(await _teamsCS.GetListAsync()) :
                          Problem("Entity set 'HRMContext.Teams'  is null.");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var team = await _teamsCS.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                await _teamsCS.AddAsync(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var team = await _teamsCS.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _teamsCS.UpdateAsync(team);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_teamsCS.Exists(team.Id))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var team = await _teamsCS.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_teamsCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Teams'  is null.");
            }
            var team = await _teamsCS.GetByIdAsync(id);
            if (team != null)
            {
                await _teamsCS.DeleteAsync(team);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
