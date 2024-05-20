using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueServices _services;
        private readonly IUserServices _userServices;
        public RevenueController(IRevenueServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }
        [HttpGet("GetOrderValueByDay")]
        public async Task<IActionResult> GetOrderValueByDate([FromQuery] int idBranch, int year, int month, int day)
        {
            var rs = await _services.GetSOValueByDay(idBranch, year, month, day);
            return Ok(rs);
        }
        [HttpGet("GetPurchaseOrderValueByDay")]
        public async Task<IActionResult> GetTodayPurchaseOrderValue([FromQuery] int idBranch, int year, int month, int day)
        {
            var rs = await _services.GetPOValueByDay(idBranch, year, month, day);
            return Ok(rs);
        }
        [HttpGet("GetRevenueByMonth")]
        public async Task<IActionResult> GetRevenueByMonth([FromQuery]int month, int idBranch)
        {
            var rs = await _services.GetRevenueByMonth(idBranch, month);
            return Ok(rs);
        }
        [HttpPost("GetRevenueByYear")]
        public async Task<IActionResult> GetRevenueByYear([FromQuery]int year, int idBranch)
        {
            var rs = await _services.GetRevenueByYear(idBranch, year);
            return Ok(rs);
        }
        [HttpGet("GetTodayOrder")]
        public async Task<IActionResult> GetTodayOrder([FromQuery] int idBranch)
        {
            var rs = await _services.GetTodayOrder(idBranch);
            return Ok(rs);
        }
        [HttpGet("GetTodayPurchaseRequest")]
        public async Task<IActionResult> GetTodayPurchaseRequest([FromQuery] int idBranch)
        {
            var rs = await _services.GetTodayPR(idBranch);
            return Ok(rs);
        }
        [HttpGet("GetTodaySoldProduct")]
        public async Task<IActionResult> GetTodaySoldProduct([FromQuery] int idBranch)
        {
            var rs = await _services.GetTodaySoldProduct(idBranch);
            return Ok(rs);
        }
        [HttpGet("GetTopProductByMonth")]
        public async Task<IActionResult> GetTopProductByMonth([FromQuery] int month, int idBranch)
        {
            var rs = await _services.GetTopSellingProduct(month, idBranch);
            return Ok(rs);
        }
        [HttpPost("GetTopBenefitProductByMonth")]
        public async Task<IActionResult> GetTopBenefitProductByMonth([FromQuery] int month, int idBranch)
        {
            var rs = await _services.GetTopBenefitProduct(month, idBranch);
            return Ok(rs);
        }
    }
}
