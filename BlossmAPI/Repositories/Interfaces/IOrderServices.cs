using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IOrderServices
    {
        public Task<ApiResponse<object>> GetOrders(int page_number);
        public Task<ApiResponse<List<Order>>> GetOrderByUserId(string id);
        public Task<ApiResponse<Order>> CreateOrder(OrderView view, string id);
        public Task<ApiResponse<Order>> UpdateOrderStatus(OrderView view);
        public Task<ApiResponse<Order>> UpdateOrderDeliveryStatus(OrderView view);
        public Task<ApiResponse<Order>> GetOrderById(int id);
    }
}