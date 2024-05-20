using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IOrderStatusServices
    {
        Task<ApiResponse<object>> GetOrderStatus();
    }
}

