using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.UserLevelsServices
{
    public class UserLevelsControlService : IGenericControlService<UserLevel>
    {
        private readonly HRMContext _context;
        public UserLevelsControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<UserLevel>> GetListAsync() => await _context.UserLevels.ToListAsync();
        public async Task<UserLevel?> GetByIdAsync(int? id) => await _context.UserLevels.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(UserLevel t)
        {
            _context.UserLevels.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(UserLevel t)
        {
            _context.UserLevels.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UserLevel t)
        {
            _context.UserLevels.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.UserLevels?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.UserLevels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
