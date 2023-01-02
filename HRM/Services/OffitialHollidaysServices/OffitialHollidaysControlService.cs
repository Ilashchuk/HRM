using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.OffitialHollidaysServices
{
    public class OffitialHollidaysControlService : IGenericControlService<OffitialHolliday>
    {
        private readonly HRMContext _context;
        public OffitialHollidaysControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<OffitialHolliday>> GetListAsync() => await _context.OffitialHollidays.ToListAsync();
        public async Task<OffitialHolliday?> GetByIdAsync(int? id) => await _context.OffitialHollidays
            .FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(OffitialHolliday t)
        {
            _context.OffitialHollidays.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(OffitialHolliday t)
        {
            _context.OffitialHollidays.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(OffitialHolliday t)
        {
            _context.OffitialHollidays.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.OffitialHollidays?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.OffitialHollidays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}