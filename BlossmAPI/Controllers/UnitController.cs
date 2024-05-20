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
    public class UnitController : ControllerBase
    {
        private readonly IUnitServices _unitServices;
        public UnitController(IUnitServices unitServices)
        {
            this._unitServices = unitServices;
        }
        [HttpGet("GetUnits")]
        public async Task<IEnumerable<UnitView>> Get()
        {
            var rs = await _unitServices.GetAll();
            return rs;
        }
        [HttpPost]
        public async Task<IActionResult> Create(UnitView new_unit)
        {
            var rs = await _unitServices.Create(new_unit);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UnitView updated_unit)
        {
            var rs = await _unitServices.Update(updated_unit);
            if (rs) return Ok();
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var rs = await _unitServices.Delete(id);
            if (rs) return Ok();
            return BadRequest();
        }
    }
}
