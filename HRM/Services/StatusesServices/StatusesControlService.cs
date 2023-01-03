using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusesServices
{
    public class StatusesControlService : IStatusesControlService
    {
        private readonly HRMContext _context;
        public StatusesControlService(HRMContext context)
        {
            _context = context;
        }

        public Status FirstForRequest() => _context.Statuses.First(st => st.Name == "Expected");
        public async Task<List<Status>?> GetByStatusTypeIdAsync(int id)
        {
            return await _context.Statuses.Where(s => s.StatusTypeId == id).ToListAsync();
        }
        public int GetIdWitValueUserStatus() => _context.StatusTypes.First(st => st.Name == "User status").Id;
        public async Task<List<Status>?> GetRequestStatusesAsync() => await _context.Statuses
            .Where(st => st.StatusType.Name == "Request status").ToListAsync();
        public async Task<List<Status>> GetListAsync() => await _context.Statuses.ToListAsync();
        public async Task<Status?> GetByIdAsync(int? id) => await _context.Statuses.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Status t)
        {
            _context.Statuses.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Status t)
        {
            _context.Statuses.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Status t)
        {
            _context.Statuses.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Statuses?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Statuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
