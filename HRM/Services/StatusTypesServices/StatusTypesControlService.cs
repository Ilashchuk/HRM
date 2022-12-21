using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusTypesServices
{
    public class StatusTypesControlService : IGenericControlService<StatusType>
    {
        private readonly HRMContext _context;
        public StatusTypesControlService(HRMContext context) => _context = context;
        public async Task<List<StatusType>> GetListAsync() => await _context.StatusTypes.ToListAsync();
        public async Task<StatusType?> GetByIdAsync(int? id) => await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == id);
        public async Task AddAsync(StatusType statusType)
        {
            _context.StatusTypes.Add(statusType);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(StatusType statusType)
        {
            _context.Update(statusType);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(StatusType statusType)
        {
            _context.Remove(statusType);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.StatusTypes?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.StatusTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
