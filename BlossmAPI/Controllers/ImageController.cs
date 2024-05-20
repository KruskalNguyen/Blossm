using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageServices _imageServices;
        public ImageController(IImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        [HttpPost("GetImagesByVariantId")]
        public IActionResult Get([FromBody] int id)
        {
            var rs = _imageServices.GetImagesByVariantId(id);
            if (rs != null ) { return Ok(rs); }
            else { return NotFound(); }
        }

        [HttpPost("CreateImages")]
        public async Task<IActionResult> Create([FromBody] ImageVIew imageView)
        {
            var rs = await _imageServices.AddImage(imageView);
            if (rs) { return Ok(rs); } else { return BadRequest(); }
        }

        [HttpPut("UpdateImage")]
        public async Task<IActionResult> Update([FromBody] ImageVIew imageView)
        {
            var rs = await _imageServices.UpdateImage(imageView);
            if (rs) { return Ok(rs); } else { return BadRequest(); }
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var rs = await _imageServices.DeleteImage(id);
            if (rs) { return Ok(rs); } else { return BadRequest(); }
        }
    }
}
