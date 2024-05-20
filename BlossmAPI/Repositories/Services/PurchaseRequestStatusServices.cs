using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlossmAPI.Repositories.Services
{
    public class PurchaseRequestStatusServices: IPurchaseRequestStatusInterfaces
    {
        private readonly BlossmContext _context;

        public PurchaseRequestStatusServices(BlossmContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> GetAllStatus()
        {
            ApiResponse<object> rs = new ApiResponse<object>();
            try
            {
                return rs.SuccessResult(
                    await _context.PurchaseRequestStatuses
                    .Select(p => new
                    {
                        p.Id,
                        p.Name
                    })
                    .ToListAsync()
                    );
            }
            catch (Exception ex)
            {
                return rs.FalseResult(ex.Message);
            }
        }
    }
}
