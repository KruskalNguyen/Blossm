using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IUserServices _userServices;
        public OrderController(IOrderServices orderServices, IUserServices userServices)
        {
            _orderServices = orderServices;
            _userServices = userServices;
        }
        [HttpPost("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders([FromBody] int page_number)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _orderServices.GetOrders(page_number);
            if (rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("GetOrderById")]
        public async Task<IActionResult> GetOrderById([FromBody] int id)
        {
            var rs = await _orderServices.GetOrderById(id);
            if (rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("GetOrdersByUserId")]
        public async Task<IActionResult> GetOrdersByUserId([FromBody] string id)
        {
            var rs = await _orderServices.GetOrderByUserId(id);
            if (rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderView view)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _orderServices.CreateOrder(view, idUser);
            if (rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("UpdateOrderDeliveryStatus")]
        public async Task<IActionResult> UpdateOrderDeliveryStatus([FromBody] OrderView view)
        {
            var rs = await _orderServices.UpdateOrderDeliveryStatus(view);
            if (rs != null)
            {
                return Ok(rs.ErrorMessage);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderView view)
        {
            var rs = await _orderServices.UpdateOrderStatus(view);
            if (rs != null)
            {
                return Ok(rs.ErrorMessage);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}