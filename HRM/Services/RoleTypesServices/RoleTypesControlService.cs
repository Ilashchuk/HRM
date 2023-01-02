using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.RoleTypesServices
{
    public class RoleTypesControlService : IGenericControlService<RoleType>
    {
        private readonly HRMContext _context;
        public RoleTypesControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<RoleType>> GetListAsync() => await _context.RoleTypes.ToListAsync();
        public async Task<RoleType?> GetByIdAsync(int? id) => await _context.RoleTypes.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(RoleType t)
        {
            _context.RoleTypes.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(RoleType t)
        {
            _context.RoleTypes.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(RoleType t)
        {
            _context.RoleTypes.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.RoleTypes?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.RoleTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

