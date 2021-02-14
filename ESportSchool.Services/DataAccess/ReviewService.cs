using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task CreateAsync(Review review, CancellationToken ct = default)
        {
            return _reviewRepository.CreateAsync(review, ct);
        }

        public Task<Review> GetAsync(int id, CancellationToken ct = default)
        {
            return _reviewRepository.GetAsync(id, ct);
        }

        public Task<List<Review>> GetAllAsync(CancellationToken ct = default)
        {
            return _reviewRepository.GetAllAsync(ct);
        }

        public Task<List<Review>> PageAsync(int skip, int take, CancellationToken ct = default)
        {
            return _reviewRepository.PageAsync(skip, take, ct);
        }

        public Task UpdateAsync(Review review, CancellationToken ct = default)
        {
            return _reviewRepository.UpdateAsync(review, ct);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return _reviewRepository.DeleteAsync(id, ct);
        }
        
        public Task DeleteAsync(Review review, CancellationToken ct = default)
        {
            return _reviewRepository.DeleteAsync(review, ct);
        }
    }
}