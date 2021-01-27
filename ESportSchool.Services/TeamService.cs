using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services
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

        public async Task CreateTeam(Team team)
        {
            await _teamRepository.AddAsync(team);
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }

        public async Task<List<Team>> GetByName(string name)
        {
            return await _teamRepository.GetByNameAsync(name);
        }

        public async Task AssignUserToTeam(User user, Team team)
        {
            user.Team = team;
            await _userRepository.UpdateAsync(user);

            await _teamRepository.AddUserAsync(team, user);
        }
    }
}