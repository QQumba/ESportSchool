using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Controllers
{
    [ApiController]
    [Route("api/jwt")]
    public class TokenTestController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetJwt()
        {
            var response = new
            {
                Name = User.Identity.Name,
                Authorized = User.Identity.IsAuthenticated
            };

            return Ok(response);
        }

        [Authorize(Roles = UserRole.Administrator)]
        [HttpGet]
        [Route("role")]
        public IActionResult GetRole()
        {
            return Ok("admin");
        }
    }
}