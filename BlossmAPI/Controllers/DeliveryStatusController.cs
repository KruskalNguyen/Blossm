using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStatusController : ControllerBase
    {
        private readonly IDeliveryStatusServices _deliveryStatusServices;

        public DeliveryStatusController(IDeliveryStatusServices deliveryStatusServices)
        {
            _deliveryStatusServices = deliveryStatusServices;
        }

        [HttpGet("GetDeliveryStatus")]
        public async Task<IActionResult> GetDeliveryStatus()
        {
            var rs = await _deliveryStatusServices.GetDeliveryStatus();
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
