using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.RequestTypesService
{
    public class RequestTipesControlService : IGenericControlService<RequestType>
    {
        private readonly HRMContext _context;
        public RequestTipesControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<RequestType>> GetListAsync() => await _context.RequestTypes.ToListAsync();
        public async Task<RequestType?> GetByIdAsync(int? id) => await _context.RequestTypes.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(RequestType t)
        {
            _context.RequestTypes.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(RequestType t)
        {
            _context.RequestTypes.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(RequestType t)
        {
            _context.RequestTypes.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.RequestTypes?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.RequestTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}