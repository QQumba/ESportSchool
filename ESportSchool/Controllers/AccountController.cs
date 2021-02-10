using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : JwtController
    {
        private readonly UserService _userService;
        private readonly TeamService _teamService;
        
        public AccountController(UserService userService, TeamService teamService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserAsync(Email);
            var coach = user.Coach;
            
            return Ok(user);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoachProfile([FromBody] CoachViewModel model)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            var profile = new Coach
            {
                GameProfiles = model.GameProfiles,
                Language = model.Language,
                User = user,
            };
            await _userService.CreateCoachAccountAsync(profile);
            return Ok();
        }
        
        [HttpPost]
        [Route("modify")]
        public async Task ModifyProfile(User user)
        {
            await _userService.UpdateUserAsync(user);
        }

        [HttpPost]
        [Route("team")]
        public async Task PickTeam([FromBody] Team team)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            user.Team = team;
            await _userService.UpdateUserAsync(user);
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> SetSchedule([FromBody] List<ScheduleInterval> schedule)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            if (user?.Coach == null)
            {
                return BadRequest(new {errorText = "Your should create coach account first."});
            }
            user.Coach.Schedule = schedule;
            await _userService.UpdateUserAsync(user);
            return Ok();
        }
    }
}