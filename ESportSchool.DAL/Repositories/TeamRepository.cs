using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(ESportSchoolDBContext context) : base(context) { }


        public async Task<List<Team>> GetByNameAsync(string name)
        {
            return await Set.Where(t => t.Title.Contains(name)).ToListAsync();
        }

        public async Task AddUserAsync(Team team, User user)
        {
            team.Users.Add(user);
            await UpdateAsync(team);
        }
    }
}