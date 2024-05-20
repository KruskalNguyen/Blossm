using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeServices _sizeServices;
        public SizeController(ISizeServices sizeServices)
        {
            _sizeServices = sizeServices;
        }
        [HttpGet("GetSizes")]
        public async Task<IEnumerable<SizeView>> GetSizes()
        {
            var rs = await _sizeServices.GetAll();
            return rs;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SizeView new_size)
        {
            var rs = await _sizeServices.Create(new_size);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SizeView updated_size)
        {
            var rs = await _sizeServices.Update(updated_size);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var rs = await _sizeServices.Delete(id);
            if (rs) return Ok();
            return BadRequest();
        }
    }
}
