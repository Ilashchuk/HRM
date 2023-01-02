using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.StatusesServices
{
    public class StatusesControlService : GenericControlService<Status>, IStatusesControlService
    {
        public StatusesControlService(HRMContext context)
            : base(context, context.Statuses)
        {

        }

        public async Task<List<Status>?> GetByStatusTypeIdAsync(int id)
        {
            return await _context.Statuses.Where(s => s.StatusTypeId == id).ToListAsync();
        }
        public int GetIdWitValueUserStatus() => _context.StatusTypes.First(st => st.Name == "User status").Id;
    }
}
