using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Services;
using ESportSchool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TeamService _teamService;
        
        public ProfileController(UserService userService, TeamService teamService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Me()
        {
            return Ok(User.Identity.Name);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoachProfile([FromBody] CoachProfileViewModel model)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            var profile = new CoachProfile
            {
                CsgoProfile = model.CsgoProfile,
                DotaProfile = model.DotaProfile,
                LolProfile = model.LolProfile,
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

        [HttpGet]
        [Route("team")]
        public async Task<IActionResult> PickTeam([FromQuery]string teamName)
        {
            var trainings = await _teamService.GetByName(teamName);
            if (trainings == null)
            {
                return BadRequest(new {errorText = "No teams with specified name"});
            }

            return Ok(trainings);
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
            if (user?.CoachProfile == null)
            {
                return BadRequest(new {errorText = "Your should create coach account first."});
            }
            user.CoachProfile.Schedule = schedule;
            await _userService.UpdateUserAsync(user);
            return Ok();
        }
    }
}