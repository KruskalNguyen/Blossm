using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IDeliveryStatusServices
    {
        Task<ApiResponse<object>> GetDeliveryStatus();
    }
}
