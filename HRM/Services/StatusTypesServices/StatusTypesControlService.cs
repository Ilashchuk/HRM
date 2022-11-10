using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusTypesServices
{
    public class StatusTypesControlService : IStatusTypesControlService
    {
        private readonly HRMContext _context;
        public StatusTypesControlService(HRMContext context) => _context = context;
        public async Task<List<StatusType>> GetStatusTypesAsync() => await _context.StatusTypes.ToListAsync();
        public async Task<StatusType?> GetStatusTypeByIdAsync(int? id) => await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == id);
        public async Task AddStatusTypeAsync(StatusType statusType)
        {
            _context.StatusTypes.Add(statusType);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStatusTypeAsync(StatusType statusType)
        {
            _context.Update(statusType);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStatusTypeAsync(StatusType statusType)
        {
            _context.Remove(statusType);
            await _context.SaveChangesAsync();
        }
        public bool StatusTypesTableIsNotEmpty()
        {
            return (_context.StatusTypes?.Any()).GetValueOrDefault();
        }
        public bool StatusTypeExists(int id)
        {
            return (_context.StatusTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
