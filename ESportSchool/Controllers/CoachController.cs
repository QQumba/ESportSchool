using System.Security.Permissions;
using System.Threading.Tasks;
using ESportSchool.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.Web.Controllers
{
    //allow user to get coach
    [Authorize]
    [Route("api/coaches")]
    public class CoachController : ControllerBase
    {
        private IConfiguration _configuration;

        public CoachController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Get()
        {
            
        }
    }
}