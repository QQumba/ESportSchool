using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private const int TeamsToTake = 6;
        public TeamRepository(ESportSchoolDbContext context) : base(context) { }


        public async Task<List<Team>> GetAsync(string name)
        {
            return await Set.Where(t => t.Title.Contains(name)).OrderBy(t => t.Title).Take(TeamsToTake).ToListAsync();
        }
    }
}