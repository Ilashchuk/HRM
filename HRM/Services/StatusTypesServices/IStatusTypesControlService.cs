using HRM.Models;

namespace HRM.Services.StatusTypesServices
{
    public interface IStatusTypesControlService
    {
        public Task<List<StatusType>> GetStatusTypesAsync();
        public Task<StatusType?> GetStatusTypeByIdAsync(int? id);
        public Task AddStatusTypeAsync(StatusType statusType);
        public Task UpdateStatusTypeAsync(StatusType statusType);
        public Task DeleteStatusTypeAsync(StatusType statusType);
        public bool StatusTypesTableIsNotEmpty();
        public bool StatusTypeExists(int id);
    }
}
