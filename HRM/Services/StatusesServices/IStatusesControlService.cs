using HRM.Models;

namespace HRM.Services.StatusesServices
{
    public interface IStatusesControlService : IGenericControlService<Status>
    {
        public Task<List<Status>?> GetByStatusTypeIdAsync(int id);
        public int GetIdWitValueUserStatus();

    }
}
