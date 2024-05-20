using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[BlossmAuthorize]
    public class BranchProductVariantController : ControllerBase
    {
        private readonly IBranchProductVariantService _branchProductVariantService;
        private readonly IUserServices _userServices;
        public BranchProductVariantController(IBranchProductVariantService branchProductVariantService, IUserServices userServices)
        {
            _branchProductVariantService = branchProductVariantService;
            _userServices = userServices;
        }

        [HttpPost("CreateBranchProductVariant")]
        public async Task<IActionResult> CreateBranchProductVariant(BranchProductVariantView mVBranchProductVariant)
        {
            var rs = await _branchProductVariantService.CreateBranchProductVariant(mVBranchProductVariant);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpPost("AddMultipleBranchProductVariant")]
        public async Task<IActionResult> AddMultipleBranchProductVariant(BranchProductVariantView mVBranchProductVariant)
        {
            var rs = await _branchProductVariantService.AddMultipleBranchProductVariant(mVBranchProductVariant);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpPut("UpdateBranchProductVariant")]
        public async Task<IActionResult> UpdateBranchProductVariant(BranchProductVariantView mVBranchProductVariant)
        {
            /*var rs = await _branchProductVariantService.UpdateBranchProductVariant(mVBranchProductVariant);
            if(rs)
                return Ok();
            return BadRequest();*/
            var id_user = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _branchProductVariantService.UpdateBranchProductVariant(id_user, mVBranchProductVariant);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpDelete("DeleteBranchProductVariant")]
        public async Task<IActionResult> DeleteBranchProductVariant(int idBranch, int idProductVariant)
        {
            var rs = await _branchProductVariantService.DeleteBranchProductVariant(idBranch, idProductVariant);
            if (rs)
                return Ok();
            return BadRequest();
        }
    }
}
