using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class PaymentMethodServieces:IPaymentMethodServieces
    {
        private readonly BlossmContext _context;

        public PaymentMethodServieces(BlossmContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> GetPaymentMethod()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var pm = await _context.PaymentMethods
                    .Select(p => new
                    {
                        p.Id,
                        p.Name
                    })
                    .ToListAsync();
                return apiResponse.SuccessResult(pm);
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.Message);
            }
        }
    }
}
