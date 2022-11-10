using HRM.Models;

namespace HRM.Services.StatusesServices
{
    public interface IStatusesControlService
    {
        public Task<List<Status>> GetStatusesAsync();
        public Task<Status?> GetStatusByIdAsync(int? id);
        public Task AddStatusAsync(Status status);
        public Task UpdateStatusAsync(Status status);
        public Task DeleteStatusAsync(Status status);
        public bool StatusesTableIsNotEmpty();
        public bool StatusExists(int id);
        public Task<List<Status>?> GetStatusesByStatusTypeIdAsync(int id);
        public int GetStatusIdWitValueUserStatus();

    }
}
