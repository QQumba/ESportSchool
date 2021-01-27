using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.NotMapped;
using ESportSchool.Services;
using ESportSchool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("training")]
    public class TrainingController : ControllerBase
    {
        private readonly TrainingService _trainingService;
        private readonly UserService _userService;

        public TrainingController(UserService userService, TrainingService trainingService)
        {
            _userService = userService;
            _trainingService = trainingService;
        }

        [HttpPost]
        [Route("coaches")]
        public async Task<IActionResult> Coaches(CoachFilterViewModel model)
        {
            var coachFilter = new CoachFilter
            {
                Start = model.Start,
                Duration = model.Duration,
                Game = model.Game,
                Language = model.Language,
            };
            await _trainingService.GetAvailableCoachesAsync(coachFilter);
            return RedirectToAction("Coaches");
        }

        [HttpPost]
        [Route("order-solo")]
        public async Task<IActionResult> OrderSoloTraining(TrainingViewModel model)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return BadRequest(new {errorText = "Authorization error."});
            }

            var participants = new List<User>() {user};

            var training = new Training()
            {
                Coach = model.CoachProfile,
                Start = model.Interval.Start,
                Duration = model.Interval.Duration,
                Participants = participants
            };
            await _trainingService.CreateTrainingAsync(training, "training/confirm/");
            return Ok();
        }

        [HttpPost]
        [Route("order-party")]
        public async Task<IActionResult> OrderPartyTraining(TrainingViewModel model)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            if (user?.Team == null)
            {
                return BadRequest(new {errorText = "Authorization error."});
            }

            var participants = user.Team.Users;

            var training = new Training()
            {
                Coach = model.CoachProfile,
                Start = model.Interval.Start,
                Duration = model.Interval.Duration,
                Participants = participants,
            };
            await _trainingService.CreateTrainingAsync(training, "training/confirm/");
            return Ok();
        }

        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmTraining(int trainingId)
        {
            var training = await _trainingService.GetTraining(trainingId);
            var user = await _userService.GetUserAsync(User.Identity.Name);
            if (!training.Coach.Equals(user.CoachProfile))
            {
                return BadRequest(new {errorText = "You are not current training coach."});
            }

            await _trainingService.ConfirmTrainingAsync(trainingId);
            return Ok();
        }
    }
}