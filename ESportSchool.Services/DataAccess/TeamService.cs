using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services.DataAccess
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public TeamService(ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        public async Task CreateTeamAsync(Team team)
        {
            await _teamRepository.CreateAsync(team);
        }

        public async Task<Team> GetTeamAsync(int id)
        {
            return await _teamRepository.GetAsync(id);
        }

        public async Task<List<Team>> GetByNameAsync(string name)
        {
            return await _teamRepository.GetAsync(name);
        }

        public async Task AssignUserToTeamAsync(User user, Team team)
        {
            user.Team = team;
            await _userRepository.UpdateAsync(user);

        }

        public async Task UpdateTeam(Team team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        public async Task DeleteTeam(Team team)
        {
            await _teamRepository.DeleteAsync(team);
        }
    }
}