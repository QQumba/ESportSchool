using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services
{
    public class CoachService
    {
        private readonly ICoachRepository _coachRepository;

        public CoachService(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public Task CreateCoachAccountAsync(Coach coach, User user)
        {
            user.Coach = coach;
            return _coachRepository.CreateAsync(coach);
        }

        public Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter)
        {
            return _coachRepository.GetAvailableCoachesAsync(filter);
        }
    }
}