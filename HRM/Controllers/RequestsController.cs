using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.Models;
using HRM.Services;
using HRM.Services.StatusesServices;
using HRM.Services.UsersServices;
using Microsoft.AspNetCore.Authorization;
using HRM.Services.RequestsServices;

namespace HRM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestsControlService _requestCS;
        private readonly IGenericControlService<RequestType> _requestTypeCS;
        private readonly IStatusesControlService _statusesCS;
        private readonly IUsersControlService _usersCS;

        public RequestsController(IRequestsControlService requestCS,
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
            var curentUser = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
            return View(await _requestCS.GetRequestListForCurrentUserAsync(curentUser));
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
        [Authorize(Roles = "TeamLead, Developer")]
        public async Task<IActionResult> Create()
        {
            //var user = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetRequestStatusesAsync(), "Id", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "TeamLead, Developer")]
        public async Task<IActionResult> Create([Bind("Id,UserId,RequestTypeId,StartDate,EndDate,StatusId")] Request request)
        {
            var curentUser = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
            if (request.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "StartDate is not Valid");
            }
            else if (request.EndDate <= request.StartDate)
            {
                ModelState.AddModelError("EndDate", "EndDate is not Valid");
            }
            if (ModelState.IsValid)
            {
                request.StatusId = _statusesCS.FirstForRequest().Id;
                request.UserId = curentUser.Id;
                bool exeption = false;
                try
                {
                    await _requestCS.AddAsync(request, curentUser);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "You have no so match days!");
                    exeption = true;
                }
                if (_requestCS.Exists(request.Id))
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (!exeption)
                {
                    ModelState.AddModelError("", "Something got wrong!");
                }
            }
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Name", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetRequestStatusesAsync(), "Id", "Name", request.StatusId);
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
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Name", request.RequestTypeId);
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Email", request.UserId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetRequestStatusesAsync(), "Id", "Name", request.StatusId);
            
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

            if (ModelState.IsValid && request.StartDate >= DateTime.Today && request.EndDate > request.StartDate)
            {
                try
                {
                    var curentUser = await _usersCS.GetUserByEmailAsync(HttpContext.User.Identity.Name);
                    if (request.StatusId == 0)
                    {
                        request.StatusId = _statusesCS.FirstForRequest().Id;
                    }
                    if (request.UserId == 0)
                    {
                        request.UserId = curentUser.Id;
                    }

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
            ViewData["RequestTypeId"] = new SelectList(await _requestTypeCS.GetListAsync(), "Id", "Name", request.RequestTypeId);
            ViewData["UserId"] = new SelectList(await _usersCS.GetListAsync(), "Id", "Email", request.UserId);
            ViewData["StatusId"] = new SelectList(await _statusesCS.GetRequestStatusesAsync(), "Id", "Name", request.StatusId);
            
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
