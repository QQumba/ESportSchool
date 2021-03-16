using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services.DataAccess;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.Web.Controllers
{
    //allow user to get coach
    [Authorize]
    [Route("api/coach")]
    public class CoachController : JwtController
    {
        private readonly IConfiguration _configuration;
        private readonly CoachService _coachService;

        public CoachController(IConfiguration configuration, CoachService coachService, UserService userService) : base(userService)
        {
            _configuration = configuration;
            _coachService = coachService;
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CoachViewModel model)
        {
            var user = await GetCurrentUserAsync();

            if (user.Coach != null)
            {
                return BadRequest("You have coach account already.");
            }

            var coach = model.Adapt<Coach>();
            user.Coach = coach;
            await _coachService.CreateAsync(coach);
            
            return Ok(coach.Id);
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var user = await GetCurrentUserAsync();
            return Ok(user.Coach);
        }

        /// <summary>
        /// Only for testing purposes
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task Delete()
        {
            var user = await GetCurrentUserAsync();
            await _coachService.DeleteAsync(user.Coach);
        }
    }
    
}