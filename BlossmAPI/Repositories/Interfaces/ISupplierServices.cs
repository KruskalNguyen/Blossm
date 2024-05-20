using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface ISupplierServices
    {
        Task<bool> AddSupplier(SupplierView mVSupplier);
        Task<ApiResponse<object>> GetSupWithPVFromUser(string id_user);
    }
}
