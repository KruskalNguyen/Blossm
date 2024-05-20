using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriovitiesController : ControllerBase
    {
        private readonly IPriovityServices _priovityServices;

        public PriovitiesController(IPriovityServices priovityServices)
        {
            _priovityServices = priovityServices;
        }

        [HttpGet("GetAllPriovity")]
        public async Task<IActionResult> GetAllPriovity()
        {
            var rs = await _priovityServices.GetAllPriovity();
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }   
}
