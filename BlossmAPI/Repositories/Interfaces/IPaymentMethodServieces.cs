using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IPaymentMethodServieces
    {
        Task<ApiResponse<object>> GetPaymentMethod();
    }
}

