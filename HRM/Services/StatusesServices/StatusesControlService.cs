using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusesServices
{
    public class StatusesControlService : IStatusesControlService
    {
        private readonly HRMContext _context;
        public StatusesControlService(HRMContext context) => _context = context;

        public async Task<List<Status>> GetStatusesAsync()
        {
            var hRMContext = _context.Statuses.Include(s => s.StatusType);
            return await hRMContext.ToListAsync();
        }
        public async Task<Status?> GetStatusByIdAsync(int? id) => await _context.Statuses.
                                                                    Include(s => s.StatusType).
                                                                    FirstOrDefaultAsync(m => m.Id == id);
        public async Task AddStatusAsync(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStatusAsync(Status status)
        {
            _context.Update(status);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStatusAsync(Status status)
        {
            _context.Remove(status);
            await _context.SaveChangesAsync();
        }
        public bool StatusesTableIsNotEmpty()
        {
            return (_context.Statuses?.Any()).GetValueOrDefault();
        }
        public bool StatusExists(int id)
        {
            return (_context.Statuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
