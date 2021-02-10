using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.DAL;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
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
        private readonly UserService _userService;

        private readonly IConfiguration _configuration;

        public TeamController(TeamService teamService, UserService userService, IConfiguration configuration)
        {
            _teamService = teamService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            var user = await _userService.GetUserAsync(Email);

            user.Team = team;
            user.IsTeamLeader = true;
            
            await _teamService.CreateTeamAsync(team);
            await _userService.UpdateUserAsync(user);
                
            return Ok(team.Id);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetUserAsync(Email);
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
        [Route("{teamId}")]
        public async Task Update([FromBody] Team team)
        {
            await _teamService.UpdateTeam(team);
        }
        
        [HttpDelete]
        [Route("{teamId}")]
        public async Task Delete([FromRoute] int teamId)
        {
            await _teamService.DeleteTeam(teamId);
        }
    }
}