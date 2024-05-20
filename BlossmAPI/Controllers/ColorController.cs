using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorServices _colorServices;
        public ColorController(IColorServices colorServices)
        {
            _colorServices = colorServices;
        }
        [HttpGet("GetColors")]
		public async Task<IEnumerable<ColorView>> Get()
        {
            var rs = await _colorServices.GetAll();
            return rs;
        }
        [HttpPost] public async Task<IActionResult> Create([FromBody] ColorView new_color)
        {
            var rs = await _colorServices.Create(new_color);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpPut] public async Task<IActionResult> Update([FromBody] ColorView updated_color)
        {
            var rs = await _colorServices.Update(updated_color);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpDelete] public async Task<IActionResult> Delete([FromBody] int id)
        {
            var rs = await _colorServices.Delete(id);
            if (rs) return Ok();
            return BadRequest();
        }
    }
}