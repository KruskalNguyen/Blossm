using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IPurchaseServices
    {
        Task<ApiResponse<bool>> AddRequest(string userID, PurchaseRequestView purchaseRequestMV);
        Task<bool> DeleteRequest(int id);
        Task<object> GetAllRequest();
        Task<object> GetRequestFromUser(string idUser);
        Task<List<PurchaseRequest>> GetRequestFromBranch(int idBranch);
        Task<bool> RejectRequest(int id, string id_user);
        Task<bool> AcceptRequest(string id_user, int id_pr);
        Task<bool> ComfirmReceive(int id_orderParchase);
        Task<ApiResponse<bool>> ComfirmPayment(ConfirmPayment confirm);
        Task<ApiResponse<bool>> UpdatePurchaseOrderStatus(string user_id, UpdatePurchaseOrderStatusView view);
        Task<bool> RealDeleteRequest(int id);
        Task<ApiResponse<object>> GetPurchaseOrderByManager(string id);
        Task<ApiResponse<object>> GetAllPurchaseOrder();
        Task<ApiResponse<object>> GetPurchaseOrderById(int id);
    }
}
