using System;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ESportSchool.Services;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await Authenticate(model);
            if (token == null)
            {
                return BadRequest(new {errorText = "Invalid email or password."});
            }

            return Ok(token.Value);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] LoginViewModel model)
        {
            var user = await _userService.GetUserAsync(model.Email);
            if (user != null) return BadRequest(new {errorText = "User with specified email already exist."});
            
            user = new User()
            {
                Email = model.Email,
                Password = model.Password
            };
            await _userService.CreateUserAsync(user);
            var token = await Authenticate(model);
            if (token == null)
            {
                throw new Exception("Can not authenticate user.");
            }

            return Ok(token.Value);
        }

        //TODO: remove in future
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<Jwt.Jwt> Authenticate(LoginViewModel model)
        {
            var user = await _userService.GetUserIfVerified(model.Email, model.Password);
            if (user == null) return null;

            var provider = new JwtProvider(_configuration);
            var token = provider.GenerateJwtToken(user);

            return token;
        }
    }
}
