using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;
using System.Security.Claims;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IUserServices
    {
        Task<string> RegisterUser(RegisterUserView register);
        Task<bool> Login(LoginUserView user);
        string GenerateTokenString(string phone);
        Task<string> GetCurrentFullNameUser(ClaimsPrincipal user);
        Task<string> GetCurrentIdUser(ClaimsPrincipal user);
        Task<ApiResponse<bool>> RegisterEmployee(RegisterEmployeeView register);
        Task<bool> LoginEmployee(LoginEmployeeView user);
        Task<string> GetPhoneByEmail(string email);
        Task<object> GetCurrentEmployee(ClaimsPrincipal user);
        Task<AspNetUser> GetCurrentUser(ClaimsPrincipal user);
        Task<AspNetUser> GetUserById(string id);
        Task<List<Employee>> GetAllEmployees();
    }
}
