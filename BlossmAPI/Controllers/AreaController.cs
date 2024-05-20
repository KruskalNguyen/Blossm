using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _areaService.GetAllArea();
            return Ok(rs);
        }
        [HttpPost("CreateArea")]
        public async Task<IActionResult> CreateArea(AreaView mVArea)
        {
            var rs = await _areaService.CreateArea(mVArea);
            if(rs)
                return Ok();
            return BadRequest();
        }
        [HttpPut("UpdateArea")]
        public async Task<IActionResult> UpdateArea(AreaView mVArea)
        {
            var rs = await _areaService.UpdateArea(mVArea);
            if(rs)
                return Ok();
            return BadRequest();
        }
        [HttpDelete("DeleteArea")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var rs = await _areaService.DeleteArea(id);
            if(rs)
                return Ok();
            return BadRequest();
        }
    }
}
