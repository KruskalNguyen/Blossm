using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IPriovityServices
    {
        Task<ApiResponse<object>> GetAllPriovity();
    }
}
