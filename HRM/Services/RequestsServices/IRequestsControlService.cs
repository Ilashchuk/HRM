using HRM.Models;

namespace HRM.Services.RequestsServices
{
    public interface IRequestsControlService : IGenericControlService<Request>
    {
        public Task<List<Request>> GetRequestListForCurrentUserAsync(User? currentUser);
    }
}
