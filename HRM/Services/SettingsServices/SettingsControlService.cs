using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.SettingsServices
{
    public class SettingsControlService : IGenericControlService<Setting>
    {
        private readonly HRMContext _context;
        public SettingsControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<Setting>> GetListAsync() => await _context.Settings.ToListAsync();
        public async Task<Setting?> GetByIdAsync(int? id) => await _context.Settings.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Setting t)
        {
            _context.Settings.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Setting t)
        {
            _context.Settings.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Setting t)
        {
            _context.Settings.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Settings?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Settings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}