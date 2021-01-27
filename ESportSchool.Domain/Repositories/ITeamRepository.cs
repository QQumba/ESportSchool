using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;

namespace ESportSchool.Domain.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<List<Team>> GetByNameAsync(string name);
        Task AddUserAsync(Team team ,User user);
    }
}