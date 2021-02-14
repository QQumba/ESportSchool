using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class CoachService
    {
        private readonly ICoachRepository _coachRepository;

        public CoachService(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public Task CreateAsync(Coach coach, CancellationToken ct = default)
        {
            return _coachRepository.CreateAsync(coach, ct);
        }

        public Task<Coach> GetAsync(int id, CancellationToken ct = default)
        {
            return _coachRepository.GetAsync(id, ct);
        }

        public Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter, CancellationToken ct = default)
        {
            return _coachRepository.GetAvailableCoachesAsync(filter, ct);
        }

        public async Task DeleteAsync(Coach coach, CancellationToken ct = default)
        {
            await _coachRepository.DeleteAsync(coach, ct);
        }
    }
}