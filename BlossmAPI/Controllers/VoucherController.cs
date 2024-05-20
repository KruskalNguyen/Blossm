using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherServices _services;
        private readonly IUserServices _userServices;
        public VoucherController(IVoucherServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _services.GetAll();
            if(rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetVoucherByUserId")]
        public async Task<IActionResult> GetVoucherByUserId()
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _services.GetVoucherByUserId(idUser);
            if(rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(VoucherView view)
        {
            var rs = await _services.Create(view);
            if( rs != null )
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("AddToUser")]
        public async Task<IActionResult> AddToUser([FromBody]string id)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _services.AddToUser(idUser, id);
            if (rs != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(VoucherView view)
        {
            var rs = await _services.Update(view);
            if (rs != null )
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("RemoveFromUser")]
        public async Task<IActionResult> RemoveFromUser(string id)
        {
            var userId = await _userServices.GetCurrentIdUser(HttpContext.User);
            var rs = await _services.RemoveFromUser(userId, id);
            if( rs != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(string id)
        {
            var rs = await _services.Remove(id);
            if(rs != null )
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
