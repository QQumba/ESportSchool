using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportSchool.Web.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

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

        [HttpGet]
        [Route("long-task")]
        public async Task<IActionResult> DoLongWork(CancellationToken ct)
        {
            _logger.LogDebug("start do long work.");
            
            await Task.Delay(10_000, ct);
            
            _logger.LogDebug("end of long work.");
            return Ok("Job is done!");
        }
    }
}