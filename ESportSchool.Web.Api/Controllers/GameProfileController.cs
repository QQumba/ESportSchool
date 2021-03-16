using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services;
using ESportSchool.Services.DataAccess;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/game-profile")]
    public class GameProfileController : JwtController
    {
        private readonly GameProfileService _gameProfileService;
        
        public GameProfileController(UserService userService, GameProfileService gameProfileService) : base(userService)
        {
            _gameProfileService = gameProfileService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] GameProfileViewModel model, CancellationToken ct)
        {
            var user = await GetCurrentUserAsync();
            if (user.Coach == null)
            {
                return BadRequest("You should create coach profile firstly.");
            }
            if (user.Coach.GameProfiles.FirstOrDefault(g => g.Game == model.Game) != null)
            {
                return BadRequest($"Profile for {model.Game} already exists.");
            }

            var gameProfile = model.Adapt<GameProfile>();
            user.Coach.GameProfiles.Add(gameProfile);

            
            return Ok();
        }
        
        
        [HttpGet]
        [Route("{gameProfileId}")]
        public async Task<IActionResult> Get([FromRoute] int gameProfileId, CancellationToken ct)
        {
            var gameProfile = await _gameProfileService.GetAsync(gameProfileId);
            return Ok();
        }
    }
}