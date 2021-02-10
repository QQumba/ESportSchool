using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace ESportSchool.Web
{
    public abstract class UserIdentifierController : Controller
    {
        protected readonly UserService UserService;
        
        public UserIdentifierController(UserService userService)
        {
            UserService = userService;
        }

        private string GetUserEmail()
        {
            var emailType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.Where(c=>c.Type == emailType).Select(c => new
            {
                c.Value
            }).FirstOrDefault();

            return claims?.Value;
        }
        
        protected async Task<User> GetCurrentUserAsync()
        {
            if (GetUserEmail() != null)
            {
                return await UserService.GetUserAsync(GetUserEmail());
            }

            return await Task.FromResult<User>(null);
        }
    }
}