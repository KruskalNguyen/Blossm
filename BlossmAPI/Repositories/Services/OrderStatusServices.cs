using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class OrderStatusServices:IOrderStatusServices
    {
        private readonly BlossmContext _context;

        public OrderStatusServices(BlossmContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> GetOrderStatus()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var status = await _context.OrderStatuses
                    .Select(s => new
                    {
                        s.Id,
                        s.Name
                    })
                    .ToListAsync();
                return apiResponse.SuccessResult(status);
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.Message);
            }
        }
    }
}
