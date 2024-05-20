using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class PriovityServices: IPriovityServices
    {
        private readonly BlossmContext _context;

        public PriovityServices(BlossmContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> GetAllPriovity() 
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                return apiResponse.SuccessResult(await _context.Priorities
                    .Select(p => new
                    {
                        p.Id, 
                        p.Name,
                        p.Note
                    })
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.Message);
            }
        }
    }
}
