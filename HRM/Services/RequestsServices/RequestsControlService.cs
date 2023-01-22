using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.RequestsServices
{
    public class RequestsControlService : IRequestsControlService
    {
        private readonly HRMContext _context;
        public RequestsControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetRequestListForCurrentUserAsync(User? currentUser)
        {
            var HR = await _context.RoleTypes.FirstOrDefaultAsync(r => r.Name == "HR");
            if (HR != null && currentUser != null)
            {
                List<Request> requests = await GetListAsync();
                if (currentUser.RoleType == HR)
                    return requests;
                return requests.Where(u => u.UserId == currentUser.Id).ToList();
            }
            return new List<Request>();
        }
        public async Task<List<Request>> GetListAsync() => await _context.Requests
            .Include(u => u.User)
            .Include(u => u.RequestType)
            .Include(u => u.Status).ToListAsync();
        public async Task<Request?> GetByIdAsync(int? id) => await _context.Requests
            .Include(u => u.User)
            .Include(u => u.RequestType)
            .Include(u => u.Status).FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Request t)
        {
            _context.Requests.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Request t, User u)
        {
            if (await CheckValueAsync(t, u))
            {
                _context.Requests.Add(t);
                await _context.SaveChangesAsync();
            }
            else
                throw new Exception();
        }
        public async Task<bool> CheckValueAsync(Request? request, User? user)
        {
            int maxDays = 20, freeDays = 20, days = 0;
            List<Request> list = await _context.Requests.Where(x => x.User.Email == user.Email).ToListAsync();
            request.RequestType = _context.RequestTypes.FirstOrDefault(x => x.Id == request.RequestTypeId);
            foreach (Request r in list)
            {
                r.RequestType = _context.RequestTypes.FirstOrDefault(x => x.Id == r.RequestTypeId);
            }

            if (request.RequestType.Name == "Illnes")
            {
                
                maxDays = _context.Settings.First(x => x.Id != null).SeekDays;

                var newList = list.Where(x => x.RequestType.Name == "Illnes");
                if (newList != null)
                {
                    foreach (Request req in newList)
                    {
                        days += CalculateDays(req);
                    }
                }

                freeDays = maxDays - days;
            }
            else if (request.RequestType.Name == "Vacations")
            {
                maxDays = _context.Settings.First(x => x.Id != null).VacationDays;

                var newList = list.Where(x => x.RequestType.Name == "Vacations");
                if (newList != null)
                {
                    foreach (Request req in newList)
                    {
                        days += CalculateDays(req);
                    }
                }
                
                freeDays = maxDays - days;
            }
            else if (request.RequestType.Name == "Unpaid leave")
            {
                maxDays = 100;
                freeDays = 100;
            }

            if (freeDays >= CalculateDays(request))
            {
                return true;
            }

            return false;
        }

        public int CalculateDays(Request? request)
        {
            int n = (request.EndDate - request.StartDate).Days;
            int days = n + 1;
            for (int i = 0; i < n; i++)
            {
                if (request.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday || request.StartDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    days--;
                }
                else if (_context.OffitialHollidays.FirstOrDefault(f => f.Date == request.StartDate.AddDays(i)) != null)
                {
                    days--;
                }
            }

            return days;
        }



        public async Task DeleteAsync(Request t)
        {
            _context.Requests.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Request t)
        {
            _context.Requests.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Requests?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}