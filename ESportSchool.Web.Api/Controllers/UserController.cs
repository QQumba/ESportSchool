using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using ESportSchool.Services.DataAccess;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels;
using ESportSchool.Web.ViewModels.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : JwtController
    {
        private readonly TeamService _teamService;
        private readonly ILogger _logger;
        
        public UserController(UserService userService,
            TeamService teamService,
            ILogger<UserController> logger) : base(userService)
        {
            _teamService = teamService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            return Ok(user.Adapt<UserViewModel>());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await UserService.GetUserAsync(userId);
            return Ok(user.Adapt<UserViewModel>());
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateProfile([FromBody] UserViewModel model)
        {
            var user = await GetCurrentUserAsync();
            
            model.Id = user.Id;
            model.Email = user.Email;
            
            if (ModelState.IsValid)
            {
                model.Adapt(user);
            }
            
            await UserService.UpdateUserAsync(user);
        }

        [HttpPost]
        [Route("team")]
        public async Task PickTeam([FromBody] Team team)
        {
            var user = await GetCurrentUserAsync();
            user.Team = team;
            await UserService.UpdateUserAsync(user);
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> SetSchedule([FromBody] List<ScheduleInterval> schedule)
        {
            var user = await UserService.GetUserAsync(User.Identity.Name);
            if (user?.Coach == null)
            {
                return BadRequest(new {errorText = "Your should create coach account first."});
            }
            user.Coach.Schedule = schedule;
            await UserService.UpdateUserAsync(user);
            return Ok();
        }
    }
}