using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpPost("CreateBranch")]
        public async Task<IActionResult> CreateBranch(BranchView mVBranch)
        {
            var rs = await _branchService.CreateBranch(mVBranch);
            if(rs)
                return Ok();
            return BadRequest();
        }

        [HttpPut("UpdateBranch")]
        public async Task<IActionResult> UpdateBranch(BranchView mVBranch)
        {
            var rs = await _branchService.UpdateBranch(mVBranch);
            if(rs)
               return Ok();
            return BadRequest();
        }
        [HttpDelete("DeleteBranch")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var rs = await _branchService.DeleteBranch(id);
            if(rs)
                return Ok();
            return BadRequest();
        }

        [HttpGet("GetAllBranch")]
        public async Task<IActionResult> GetAllBranch()
        {
            var rs = await _branchService.GetAllBranch();
            return Ok(rs);
        }
        [HttpGet("GetQuantityPVinBranch")]
        public async Task<IActionResult> GetQuantityPVinBranch([FromQuery] int idPV, int idBranch)
        {
            var rs = await _branchService.GetQuantityPVinBranch(idPV, idBranch);
            return Ok(rs);
        }
    }
}
