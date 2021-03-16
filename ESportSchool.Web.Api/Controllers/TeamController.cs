using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.DAL;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using ESportSchool.Services.DataAccess;
using ESportSchool.Web.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/team")]
    public class TeamController : JwtController
    {
        private readonly TeamService _teamService;

        private readonly IConfiguration _configuration;

        public TeamController(TeamService teamService, UserService userService, IConfiguration configuration) : base(userService)
        {
            _teamService = teamService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            var user = await GetCurrentUserAsync();

            await _teamService.CreateTeamAsync(team);
            user.Team = team;
            user.IsTeamLeader = true;
            
            await UserService.UpdateUserAsync(user);
                
            return Ok(team.Id);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var user = await GetCurrentUserAsync();
            return Ok(user.Team);
        }

        [HttpGet]
        [Route("{teamName}")]
        public async Task<IActionResult> PickTeam([FromRoute] string teamName)
        {
            var teams = await _teamService.GetByNameAsync(teamName);
            if (teams == null)
            {
                return BadRequest(new {errorText = "No teams with specified name"});
            }

            return Ok(teams);
        }

        [HttpPut]
        [Route("")]
        public async Task Update([FromBody] Team teamModel)
        {
            var user = await GetCurrentUserAsync();
            if (user.IsTeamLeader)
            {
                var team = user.Team;
                team.Title = teamModel.Title;
                team.Description = teamModel.Description;
                
                await _teamService.UpdateTeam(team);
            }
        }
        
        [HttpDelete]
        [Route("")]
        public async Task Delete()
        {
            var user = await GetCurrentUserAsync();
            if (user.IsTeamLeader)
            {
                user.IsTeamLeader = false;
                await _teamService.DeleteTeam(user.Team);
            }
        }
    }
}