using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public class CoachRepository : Repository<Coach>, ICoachRepository
    {
        public CoachRepository(ESportSchoolDbContext context) : base(context)
        {
        }

        public async Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter)
        {
            List<Coach> coaches = null;
            coaches = await Set.Where(c => c.GameProfiles.FirstOrDefault(p => p.Game == filter.Game) != null)
                .Where(c => c.Language == filter.Language)
                .ToListAsync();

            return coaches?.Where(c => c.IsScheduleIntervalAvailable(new ScheduleInterval()
            {
                Start = filter.Start,
                Duration = filter.Duration,
            })).ToList();
        }
    }
}