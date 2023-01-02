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
using HRM.Services.StatusesServices;
using HRM.Services.UsersServices;

namespace HRM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IGenericControlService<Request> _requestCS;
        private readonly IGenericControlService<RequestType> _requestTypeCS;
        private readonly IStatusesControlService _statusesCS;
        private readonly IUsersControlService _usersCS;

        public RequestsController(IGenericControlService<Request> requestCS,
            IGenericControlService<RequestType> requestTypeCS,
            IStatusesControlService statusesCS,
            IUsersControlService usersCS)
        {
            _requestCS = requestCS;
            _requestTypeCS = requestTypeCS;
            _statusesCS = statusesCS;
            _usersCS = usersCS;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            return View(await _requestCS.GetListAsync());
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var request = await _requestCS.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Id");
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetListAsync(), "Id", "Id");
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Id");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RequestTypeId,StartDate,EndDate,StatusId")] Request request)
        {
            if (ModelState.IsValid)
            {
                await _requestCS.AddAsync(request);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Id", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetListAsync(), "Id", "Id", request.StatusId);
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Id", request.UserId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var request = await _requestCS.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Id", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetListAsync(), "Id", "Id", request.StatusId);
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Id", request.UserId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RequestTypeId,StartDate,EndDate,StatusId")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _requestCS.UpdateAsync(request);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_requestCS.Exists(request.Id))
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
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Id", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetListAsync(), "Id", "Id", request.StatusId);
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Id", request.UserId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var request = await _requestCS.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_requestCS.NotEmpty())
            {
                return Problem("Entity set 'HRMContext.Requests'  is null.");
            }
            var request = await _requestCS.GetByIdAsync(id);
            if (request != null)
            {
                await _requestCS.DeleteAsync(request);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
