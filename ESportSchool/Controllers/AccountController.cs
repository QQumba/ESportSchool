using System;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ESportSchool.Services;
using ESportSchool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.Web.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public AccountController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthorizationViewModel model)
        {
            var token = await Authenticate(model);
            if (token == null)
            {
                return BadRequest(new {errorText = "Invalid email or password."});
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AuthorizationViewModel model)
        {
            var user = await _userService.GetUserAsync(model.Email);
            if (user == null)
            {
                user = new User()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                await _userService.CreateNewUserAsync(user);
                var token = await Authenticate(model);
                if (token == null)
                {
                    throw new Exception("Can not authenticate user.");
                }

                return Ok(token);
            }

            return BadRequest(new {errorText = "User with specified email already exist."});
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<string> Authenticate(AuthorizationViewModel model)
        {
            var user = await _userService.GetUserIfVerified(model.Email, model.Password);
            if (user == null) return null;

            var provider = new JwtProvider(_configuration);
            var token = provider.GenerateJwtToken(user);

            return await Task.FromResult(token);
        }

    }
}
