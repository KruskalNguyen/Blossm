using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseRequestStatusController : ControllerBase
    {
        private readonly IPurchaseRequestStatusInterfaces _purchaseRequestStatusInterfaces;

        public PurchaseRequestStatusController(IPurchaseRequestStatusInterfaces purchaseRequestStatusInterfaces)
        {
            _purchaseRequestStatusInterfaces = purchaseRequestStatusInterfaces;
        }

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            var rs = await _purchaseRequestStatusInterfaces.GetAllStatus();
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
