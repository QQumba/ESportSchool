using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using ESportSchool.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Jwt
{
    public abstract class JwtController : ControllerBase
    {
        private readonly UserService _userService;

        protected JwtController(UserService userService)
        {
            _userService = userService;
        }

        protected UserService UserService => _userService;
        protected string Email => User.Identity.Name;

        protected Task<User> GetCurrentUserAsync(CancellationToken ct = default)
        {
            return _userService.GetUserAsync(Email, ct);
        }
    }
}