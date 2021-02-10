using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<List<Team>> GetAsync(string name);
    }
}