using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IVoucherServices
    {
        public Task<ApiResponse<bool>> Create(VoucherView view);
        public Task<ApiResponse<bool>> AddToUser(string idUser, string idVoucher);
        public Task<ApiResponse<bool>> RemoveFromUser(string idUser, string idVoucher);
        public Task<ApiResponse<bool>> Update(VoucherView view);
        public Task<ApiResponse<List<Voucher>>> GetAll();
        public Task<ApiResponse<List<Voucher>>> GetVoucherByUserId(string id);
        public Task<ApiResponse<bool>> Remove(string id);
    }
}
