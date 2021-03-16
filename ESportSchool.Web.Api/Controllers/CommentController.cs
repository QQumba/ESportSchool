using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Services.DataAccess;
using ESportSchool.Web.Jwt;
using ESportSchool.Web.ViewModels.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportSchool.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/comment")]
    public class CommentController : JwtController
    {
        private readonly CommentService _commentService;
        private readonly CoachService _coachService;
        private readonly ILogger _logger;
        
        public CommentController(UserService userService, ILogger<CommentController> logger, CommentService commentService, CoachService coachService) : base(userService)
        {
            _logger = logger;
            _commentService = commentService;
            _coachService = coachService;
        }

        [HttpPost]
        [Route("{coachId}")]
        public async Task<IActionResult> Create([FromBody] CommentViewModel model, [FromRoute] int coachId, CancellationToken ct)
        {
            var comment = model.Adapt<Comment>();
            var coach = await _coachService.GetAsync(coachId, ct);
            if (coach == null)
            {
                return BadRequest();
            }
            
            var user = await GetCurrentUserAsync();
            if (user.Coach?.Id == coachId)
            {
                return BadRequest();
            }
            
            comment.Coach = coach;
            comment.User = user;
            
            await _commentService.CreateAsync(comment, ct);
            return Ok(comment.Id);
        }
        
        [HttpGet]
        [Route("{commentId}")]
        public async Task<IActionResult> Get([FromRoute] int commentId, CancellationToken ct)
        {
            var comment = await _commentService.GetAsync(commentId, ct);
            if (comment == null)
            {
                return BadRequest("Comment with specified id does not exists.");
            }
 
            var commentViewModel = comment.Adapt<CommentViewModel>();
            return Ok(commentViewModel);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetUserComments([FromRoute] int userId, CancellationToken ct)
        {
            var comments = await _commentService.GetUserCommentsAsync(userId, ct);
            return Ok(comments.Adapt<List<CommentViewModel>>());
        }

        [HttpGet]
        [Route("coach/{coachId}")]
        public async Task<IActionResult> GetCoachComments([FromRoute] int coachId, CancellationToken ct)
        {
            var comments = await _commentService.GetUserCommentsAsync(coachId, ct);
            return Ok(comments.Adapt<List<CommentViewModel>>());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CommentViewModel model, CancellationToken ct)
        {
            var comment = await _commentService.GetAsync(model.Id, ct);
            if (comment == null)
            {
                //todo: add error message
                return BadRequest();
            }
            await _commentService.CreateAsync(comment, ct);
            return Ok(comment.Id);
        }
        
        [HttpDelete]
        [Route("{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId,CancellationToken ct)
        {
            await _commentService.DeleteAsync(commentId, ct);
            return Ok();
        }
    }
}