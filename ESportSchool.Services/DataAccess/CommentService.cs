using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICoachRepository _coachRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, ICoachRepository coachRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _coachRepository = coachRepository;
        }
        
        public async Task CreateAsync(Comment comment, CancellationToken ct = default)
        {
            await _commentRepository.CreateAsync(comment, ct);
        }

        public Task<Comment> GetAsync(int id, CancellationToken ct = default)
        {
            return _commentRepository.GetAsync(id, ct);
        }

        //todo: delete, test method
        public Task<List<Comment>> GetAllAsync()
        {
            return _commentRepository.GetAllAsync();
        }

        public async Task<List<Comment>> GetCoachCommentsAsync(int coachId, CancellationToken ct = default)
        {
            var coach = await _coachRepository.GetAsync(coachId, ct);
            return coach.Comments;
        }

        public async Task<List<Comment>> GetUserCommentsAsync(int userId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetAsync(userId, ct);
            return user.Comments;
        }
        
        // public async Task<List<Comment>> PageCoachCommentsAsync(int coachId, int skip, int take, CancellationToken ct = default)
        // {
        //     var comments = await _commentRepository.GetAllAsync(ct);
        //     return comments.Where(c => c.Coach.Id == coachId).Skip(skip).Take(take).ToList();
        // }
        //
        // public async Task<List<Comment>> PageUserCommentsAsync(int userId, int skip, int take, CancellationToken ct = default)
        // {
        //     var comments = await _commentRepository.GetAllAsync(ct);
        //     return comments.Where(c => c.User.Id == userId).Skip(skip).Take(take).ToList();
        // }

        public async Task UpdateAsync(Comment comment, CancellationToken ct = default)
        {
            comment.Edited = true;
            await _commentRepository.UpdateAsync(comment, ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var comment = await GetAsync(id, ct);
            await _commentRepository.DeleteAsync(comment, ct);
        }

        public async Task DeleteAsync(Comment comment, CancellationToken ct = default)
        {
            await _commentRepository.DeleteAsync(comment, ct);
        }
    }
}