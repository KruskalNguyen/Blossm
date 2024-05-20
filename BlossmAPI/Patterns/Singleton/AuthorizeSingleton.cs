using BlossmAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlossmAPI.Patterns.Singleton
{
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    //BIG BUG
    public sealed class AuthorizeSingleton
    {
        private static volatile AuthorizeSingleton instance = null;
        private static readonly object lockObject = new object();
        private static AuthorizationFilterContext _context;
        private static ClaimsPrincipal _user;
        private static string idUser;
        private static string fullName;
        private static ICollection<Access> _accessList;
        private static BlossmContext _dbContext;

        private AuthorizeSingleton()
        {
            
        }
        public static AuthorizeSingleton Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new AuthorizeSingleton();
                    }
                    return instance;
                }
            }
        }

        // Phương thức để truy vấn và lấy dữ liệu từ BlossmContext
        public bool checkAccess(string actionName)
        {
            if(_accessList == null)
            {
                var userDb = _dbContext.AspNetUsers
                    .Include(u => u.IdAccesses)
                    .Select(u => new
                    {
                        PhoneNumber = u.PhoneNumber,
                        Access = u.IdAccesses,
                        Id = u.Id,
                        FirstName = u.FirstName, 
                        LastName = u.LastName
                    })
                    .FirstOrDefault(u => u.PhoneNumber == _user.FindFirst(ClaimTypes.MobilePhone).Value);

                idUser = userDb.Id;
                fullName = userDb.LastName + " " + userDb.FirstName;
                _accessList = userDb.Access;

            }

            Access access = _accessList.FirstOrDefault(a => a.Action == actionName);

            if (access == null)
                return false;
            else
                return true;
        }

        public ClaimsPrincipal getUser(AuthorizationFilterContext context)
        {
            if (_user == null && _context == null)
            {
                _context = context;
                _user = _context.HttpContext.User;

                var serviceProvider = _context.HttpContext.RequestServices;

                // Lấy DbContext từ IServiceProvider
                _dbContext = serviceProvider.GetRequiredService<BlossmContext>();
            }
            return _user;
        }

        public ClaimsPrincipal getUser()
        {
            return _user;
        }

        public string getIdUser()
        {
            return idUser;
        }

        public string getFullName()
        {
            return fullName;
        }

        public void Reset()
        {
            _accessList = null;
            _context = null;
            _user = null;
            idUser = null;
        }
    }
}
