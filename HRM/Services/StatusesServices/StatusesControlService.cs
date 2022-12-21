using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusesServices
{
    public class StatusesControlService : IStatusesControlService
    {
        private readonly HRMContext _context;
        public StatusesControlService(HRMContext context) => _context = context;

        public async Task<List<Status>> GetListAsync() => await _context.Statuses.Include(s => s.StatusType).ToListAsync();
        public async Task<Status?> GetByIdAsync(int? id) => await _context.Statuses.
                                                                    Include(s => s.StatusType).
                                                                    FirstOrDefaultAsync(m => m.Id == id);
        public async Task UpdateAsync(Status status)
        {
            _context.Update(status);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Status status)
        {
            _context.Remove(status);
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

        public async Task<List<Status>?> GetByStatusTypeIdAsync(int id)
        {
            return await _context.Statuses.Where(s => s.StatusTypeId == id).ToListAsync();
        }
        public int GetIdWitValueUserStatus() => _context.StatusTypes.First(st => st.Name == "User status").Id;
        public async Task AddAsync(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
        }

    }
}
