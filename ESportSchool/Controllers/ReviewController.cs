using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
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
    [Route("api/review")]
    public class ReviewController : JwtController
    {
        private readonly ReviewService _reviewService;

        public ReviewController(UserService userService, ReviewService reviewService) : base(userService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] ReviewViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var review = model.Adapt<Review>();
            review.User = user;

            await _reviewService.CreateAsync(review);
            return Ok(review.Id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{reviewId}")]
        public async Task<IActionResult> Get(int reviewId, CancellationToken ct)
        {
            var review = await _reviewService.GetAsync(reviewId, ct);
            return Ok(review.Adapt<ReviewViewModel>());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{skip}-{take}")]
        public async Task<IActionResult> Page([FromRoute] int skip, [FromRoute] int take, CancellationToken ct)
        {
            var reviews = await _reviewService.PageAsync(skip, take, ct);
            return Ok(reviews.Adapt<List<ReviewViewModel>>());
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] ReviewViewModel model, CancellationToken ct)
        {
            var review = await _reviewService.GetAsync(model.Id, ct);
            model.Id = review.Id;
            model.Adapt(review);

            await _reviewService.UpdateAsync(review, ct);
            return Ok(review.Id);
        }

        [HttpDelete]
        [Route("{reviewId}")]
        public async Task<IActionResult> Update([FromRoute] int reviewId, CancellationToken ct)
        {
            var user = await GetCurrentUserAsync(ct);
            var review = await _reviewService.GetAsync(reviewId, ct);
            if (review.User.Id != user.Id)
            {
                return BadRequest("You are not owner of specified review.");
            }

            await _reviewService.DeleteAsync(review, ct);
            return Ok();
        }
    }
}