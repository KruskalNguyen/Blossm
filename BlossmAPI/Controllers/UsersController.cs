using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UsersController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserView register)
        {
            var rs = await _userService.RegisterUser(register);
            if (rs == "Success")
                return Ok(rs);
            else
                return BadRequest(rs);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserView user)
        {
            
            if (!ModelState.IsValid)
                return BadRequest();
           
            if (await _userService.Login(user))
            {
                var tokenString = _userService.GenerateTokenString(user.PhoneNumber);
                return Ok(tokenString);
            }
            return BadRequest();
        }

        [HttpPost("LoginEmployee")]
        public async Task<IActionResult> LoginEmployee(LoginEmployeeView user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _userService.LoginEmployee(user))
            {
                var phone = await _userService.GetPhoneByEmail(user.Email);
                var tokenString = _userService.GenerateTokenString(phone);
                return Ok(tokenString);
            }
            return BadRequest();
        }

        [HttpGet("GetCurrentFullNameUser")]
        public async Task<string> GetCurrentFullNameUser()
        {
            return await _userService.GetCurrentFullNameUser(HttpContext.User);
        }
        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeView register)
        {
            var rs = await _userService.RegisterEmployee(register);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpGet("GetCurrentEmployee")]
        public async Task<IActionResult> GetCurrentEmployee()
        {
            var rs = await _userService.GetCurrentEmployee(HttpContext.User);
            return ResponseHelper.IfNotFound(rs);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var rs = await _userService.GetCurrentUser(HttpContext.User);
            return ResponseHelper.IfNotFound(rs);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery]string id)
        {
            var rs = await _userService.GetUserById(id);
            return ResponseHelper.IfNotFound(rs);
        }
        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var rs = await _userService.GetAllEmployees();
            return Ok(rs);
        }
    }
}
