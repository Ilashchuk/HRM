using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.CompaniesServices
{
    public class CompaniesControlService : IGenericControlService<Company>
    {
        private readonly HRMContext _context;
        public CompaniesControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<Company>> GetListAsync() => await _context.Companies.ToListAsync();
        public async Task<Company?> GetByIdAsync(int? id) => await _context.Companies.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Company t)
        {
            _context.Companies.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Company t)
        {
            _context.Companies.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Company t)
        {
            _context.Companies.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Companies?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}