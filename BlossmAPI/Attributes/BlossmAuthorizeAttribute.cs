using BlossmAPI.Models;
using BlossmAPI.Patterns.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlossmAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BlossmAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public BlossmAuthorizeAttribute()
        {
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Kiểm tra xem đang ở action nào
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null)
            {
                //var controllerName = actionDescriptor.ControllerName;
                var actionName = actionDescriptor.ActionName;

                var serviceProvider = context.HttpContext.RequestServices;
                var dbContext = serviceProvider.GetRequiredService<BlossmContext>();

                var userDb = dbContext.AspNetUsers
                   .Include(u => u.IdAccesses)
                   .Select(u => new
                   {
                       PhoneNumber = u.PhoneNumber,
                       Access = u.IdAccesses,
                   })
                   .FirstOrDefault(u => u.PhoneNumber == user.FindFirst(ClaimTypes.MobilePhone).Value);

                Access access = userDb.Access.FirstOrDefault(a => a.Action == actionName);

                /*if (access == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }*/
            }

            
        }
    }
}
