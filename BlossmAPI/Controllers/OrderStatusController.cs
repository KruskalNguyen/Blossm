using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusServices _orderStatusServices;

        public OrderStatusController(IOrderStatusServices orderStatusServices)
        {
            _orderStatusServices = orderStatusServices;
        }

        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var rs = await _orderStatusServices.GetOrderStatus();
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
