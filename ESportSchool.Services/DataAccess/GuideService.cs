using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class GuideService
    {
        private readonly IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public Task CreateAsync(Guide guide, CancellationToken ct = default)
        {
            return _guideRepository.CreateAsync(guide, ct);
        }

        public Task<Guide> GetAsync(int id, CancellationToken ct = default)
        {
            return _guideRepository.GetAsync(id, ct);
        }

        public async Task<List<Guide>> GetAllAsync(int userId, CancellationToken ct = default)
        {
            var guides = await _guideRepository.GetAllAsync(ct);
            return guides.Where(g => g.User.Id == userId).ToList();
        }

        public async Task<List<Guide>> PageAsync(Game game, int skip, int take, CancellationToken ct = default)
        {
            var guides = await _guideRepository.GetAllAsync(ct);
            return guides.Where(g => g.Game == game).ToList();
        }

        public Task UpdateAsync(Guide guide, CancellationToken ct = default)
        {
            return _guideRepository.UpdateAsync(guide, ct);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return _guideRepository.DeleteAsync(id, ct);
        }

        public Task DeleteAsync(Guide guide, CancellationToken ct = default)
        {
            return _guideRepository.DeleteAsync(guide, ct);
        }
    }
}