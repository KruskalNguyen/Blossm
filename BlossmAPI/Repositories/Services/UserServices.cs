using BlossmAPI.Attributes;
using BlossmAPI.Identities;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlossmAPI.Repositories.Services
{
    public class UserServices:IUserServices
    {
        private readonly UserManager<UserAddProperties> _userManager;
        private readonly IConfiguration _config;
        private readonly BlossmContext _context;
        public UserServices(
            UserManager<UserAddProperties> userManager, 
            IConfiguration config,
            BlossmContext context)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
        }

        public async Task<string> RegisterUser(RegisterUserView register)
        {
            if(register.Password != register.RePassword)
                return "Passwords do not match";

            var identityUser = new UserAddProperties
            {
                UserName = register.PhoneNumber,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };

            var result = await _userManager.CreateAsync(identityUser, register.Password);
            if(result.Succeeded)
                return "Success";

            return "Error";
        }

        public async Task<ApiResponse<bool>> RegisterEmployee(RegisterEmployeeView register)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            if (register.Password != register.RePassword)
                return apiResponse.FalseResult("Passwords do not match");

            var identityUser = new UserAddProperties
            {
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };

            var result = await _userManager.CreateAsync(identityUser, register.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(register.Email);
                Employee employee = new Employee();
                employee.IdUser = user.Id;
                employee.Active = true;
                employee.StartDate = register.StartDate;
                employee.EndDate = register.EndDate;
                employee.IdBranch = register.Branch;

                try
                {
                    await _context.Employees.AddAsync(employee);
                    await _context.SaveChangesAsync();
                    return apiResponse.SuccessResult();
                }
                catch (Exception ex)
                {
                    return apiResponse.FalseResult($"Error: {ex.Message}");
                }
            }
               
            return apiResponse.FalseResult("Error: Tạm thời chưa biết lỗi gì");
        }

        public async Task<bool> Login(LoginUserView user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.PhoneNumber);
            if (identityUser is null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> LoginEmployee(LoginEmployeeView user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public string GenerateTokenString(string phone)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.MobilePhone,phone)
                }; 
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<string> GetCurrentFullNameUser(ClaimsPrincipal user)
        {
            if (user.Claims.Count() == 0)
                return null;

            var phone = user.FindFirst(ClaimTypes.MobilePhone).Value;

            var userDB = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return $"{userDB.FirstName} {userDB.LastName}";
            
        }

        public async Task<string> GetCurrentIdUser(ClaimsPrincipal user)
        {
            if (user.Claims.Count() == 0)
                return null;

            var username = user.FindFirst(ClaimTypes.MobilePhone).Value;

            var userDB = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(username));
            return userDB.Id;

        }

        public async Task<AspNetUser> GetCurrentUser(ClaimsPrincipal user)
        {
            if (user.Claims.Count() == 0)
                return null;

            var phone = user.FindFirst(ClaimTypes.MobilePhone).Value;

            var userDB = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(phone));
            return userDB;

        }

        public async Task<AspNetUser> GetUserById(string id)
        {
            var userDB = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id.Equals(id));
            return userDB;

        }

        public async Task<string> GetPhoneByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return user.PhoneNumber;
            else
                return "Error";

        }

        public async Task<object> GetCurrentEmployee(ClaimsPrincipal user)
        {
            if (user.Claims.Count() == 0)
                return null;

            var phone = user.FindFirst(ClaimTypes.MobilePhone).Value;

            var userDB = await _context.AspNetUsers
                .Include(u => u.EmployeeIdUserNavigation.IdBranchNavigation.ManagerNavigation)
                .Include(u => u.EmployeeIdUserNavigation.IdBranchNavigation.IdAreaNavigation)
                .Include(u => u.EmployeeIdUserNavigation.ManagerByNavigation)
                .Include(u => u.Roles)
                .Select(u => new
                {
                    idUser = u.EmployeeIdUserNavigation.IdUser,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.PhoneNumber,
                    u.EmployeeIdUserNavigation.StartDate,
                    u.EmployeeIdUserNavigation.EndDate,
                    idBranch = u.EmployeeIdUserNavigation.IdBranchNavigation.Id,
                    u.EmployeeIdUserNavigation.IdBranchNavigation.Address,
                    u.EmployeeIdUserNavigation.IdBranchNavigation.IdAreaNavigation.Name,
                    FirstNameManager = u.EmployeeIdUserNavigation.ManagerByNavigation.FirstName,
                    LastNameManager = u.EmployeeIdUserNavigation.ManagerByNavigation.LastName,
                    EmailManager = u.EmployeeIdUserNavigation.ManagerByNavigation.Email,
                    PhoneManager = u.EmployeeIdUserNavigation.ManagerByNavigation.PhoneNumber,
                    BranchManager = u.Branches.ToList()[0].Manager,
                    Roles = u.Roles.ToList()
                })
                .FirstOrDefaultAsync(u => u.PhoneNumber == phone);

            return userDB;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var rs = await _context.Employees
                .Include(e => e.IdUserNavigation)
                .ToListAsync();

            foreach(var  emp in rs)
            {
                emp.IdUserNavigation.EmployeeIdUserNavigation = null;
                emp.IdUserNavigation.EmployeeManagerByNavigations = null;
            }

            return rs;
        }
    }
}
