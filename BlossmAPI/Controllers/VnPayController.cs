using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnpayServices _vnPayService;

        public VnPayController(IVnpayServices vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("GetPaymentUrl")]
        public async Task<string> GetPaymentUrl([FromBody]int id)
        {
            var rs = await _vnPayService.CreatePaymentUrl(id);
            return rs;
        }
    }
}
