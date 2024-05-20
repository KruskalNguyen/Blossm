using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodServieces _paymentMethodServieces;

        public PaymentMethodController(IPaymentMethodServieces paymentMethodServieces)
        {
            _paymentMethodServieces = paymentMethodServieces;
        }

        [HttpGet("GetPaymentMethod")]
        public async Task<IActionResult> GetPaymentMethod() {
            var rs = await _paymentMethodServieces.GetPaymentMethod();
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
