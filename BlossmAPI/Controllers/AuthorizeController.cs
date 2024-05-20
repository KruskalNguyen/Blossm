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
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeServices _authorizeService;

        public AuthorizeController(IAuthorizeServices authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet("GetAllPrivileges")]
        public async Task<IActionResult> GetAllPrivileges()
        {
            var rs = await _authorizeService.GetAllPrivileges();
            if (rs == null)
                return NotFound();
            return Ok(rs);  
        }

        [HttpGet("GetPrivilegesFromUser")]
        public async Task<IActionResult> GetPrivilegesFromUser(string id)
        {
            var rs = await _authorizeService.GetPrivilegesFromUser(id);
            if (rs == null)
                return NotFound();
            return Ok(rs);
        }

        [HttpGet("GetNonPrivilegesFromUser")]
        public async Task<IActionResult> GetNonPrivilegesFromUser(string id)
        {
            var rs = await _authorizeService.GetNonPrivilegesFromUser(id);
            if (rs == null)
                return NotFound();
            return Ok(rs);
        }

        [HttpPost("GrantPrivilegesToUser")]
        public async Task<IActionResult> GrantPrivilegesToUser(AccessUserView accessUser)
        {
            var rs = await _authorizeService.GrantPrivilegesToUser(accessUser);
            return ResponseHelper.IfBad(rs);
        }

        [HttpDelete("RemoveAllPrivilegesOfUser")]
        public async Task<IActionResult> RemoveAllPrivilegesOfUser(string id_user)
        {
            var rs = await _authorizeService.RemoveAllPrivilegesOfUser(id_user);
            return ResponseHelper.IfBad(rs);
        }

        [HttpDelete("RemovePrivilegesOfUser")]
        public async Task<IActionResult> RemovePrivilegesOfUser(AccessUserView accessUser)
        {
            var rs = await _authorizeService.RemovePrivilegesOfUser(accessUser);
            return ResponseHelper.IfBad(rs);
        }

        [HttpGet("GetPrivilegesFromRole")]
        public async Task<IActionResult> GetPrivilegesFromRole(string id)
        {
            var rs = await _authorizeService.GetPrivilegesFromRole(id);
            if (rs == null)
                return NotFound();
            return Ok(rs);
        }

        [HttpGet("GetNonPrivilegesFromRole")]
        public async Task<IActionResult> GetNonPrivilegesFromRole(string id)
        {
            var rs = await _authorizeService.GetNonPrivilegesFromRole(id);
            if (rs == null)
                return NotFound();
            return Ok(rs);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleView mvRole)
        {
            var rs = await _authorizeService.CreateRole(mvRole);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("GrantPrivilegesToRole")]
        public async Task<IActionResult> GrantPrivilegesToRole(AccessRoleView accessRole)
        {
            var rs = await _authorizeService.GrantPrivilegesToRole(accessRole);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("GrantRoleToUser")]
        public async Task<IActionResult> GrantRoleToUser(UserRoleView userRole)
        {
            var rs = await _authorizeService.GrantRoleToUser(userRole);
            return ResponseHelper.IfBad(rs);
        }

        [HttpDelete("RomoveRole")]
        public async Task<IActionResult> RomoveRole(string id_role)
        {
            var rs = await _authorizeService.RomoveRole(id_role);
            return ResponseHelper.IfBad(rs);
        }

        [HttpDelete("RemovePrivilegesOfRole")]
        public async Task<IActionResult> RemovePrivilegesOfRole(AccessRoleView accessRole)
        {
            var rs = await _authorizeService.RemovePrivilegesOfRole(accessRole);
            return ResponseHelper.IfBad(rs);
        }
    }
}
