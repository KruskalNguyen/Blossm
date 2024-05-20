using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IPurchaseRequestStatusInterfaces
    {
        Task<ApiResponse<object>> GetAllStatus();
    }
}
