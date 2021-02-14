using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class GameProfileService
    {
        private readonly IGameProfileRepository _gameProfileRepository;
        private readonly ICoachRepository _coachRepository;

        public GameProfileService(IGameProfileRepository gameProfileRepository, ICoachRepository coachRepository)
        {
            _gameProfileRepository = gameProfileRepository;
            _coachRepository = coachRepository;
        }

        public async Task CreateAsync(GameProfile gameProfile, CancellationToken ct = default)
        {
            await _gameProfileRepository.CreateAsync(gameProfile, ct);
        }
        
        public Task<GameProfile> GetAsync(int id, CancellationToken ct = default)
        {
            return _gameProfileRepository.GetAsync(id, ct);
        }

        public async Task<List<GameProfile>> GetAllAsync(int coachId, CancellationToken ct = default)
        {
            var coach = await _coachRepository.GetAsync(coachId, ct);
            return coach.GameProfiles;
        }

        public async Task UpdateAsync(GameProfile gameProfile, CancellationToken ct = default)
        {
            await _gameProfileRepository.UpdateAsync(gameProfile, ct);
        }

        public Task DeleteAsync(GameProfile gameProfile, CancellationToken ct = default)
        {
            return _gameProfileRepository.DeleteAsync(gameProfile, ct);
        }
        
    }
}