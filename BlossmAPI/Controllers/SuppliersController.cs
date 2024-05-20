using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Patterns.Singleton;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierServices _supplierService;
        private readonly IUserServices _userService;

        public SuppliersController(ISupplierServices supplierService, IUserServices userServices)
        {
            _supplierService = supplierService;
            _userService = userServices;
        }

        [HttpPost("AddSupplier")]
        public async Task<IActionResult> addSupplier(SupplierView mVSupplier)
        {
            var id = AuthorizeSingleton.Instance.getIdUser();
            var rs = await _supplierService.AddSupplier(mVSupplier);
            if (rs)
                return Ok();
            else return BadRequest();
        }

        [HttpGet("GetSupWithPVFromUser")]
        public async Task<IActionResult> GetSupWithPVFromUser()
        {
            var userId = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _supplierService.GetSupWithPVFromUser(userId);
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
