using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.NotMapped;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ESportSchool.DAL.Repositories
{
    public class CoachProfileRepository : Repository<CoachProfile>, ICoachProfileRepository
    {
        public CoachProfileRepository(ESportSchoolDBContext context) : base(context)
        {
        }

        public async Task<List<CoachProfile>> GetAvailableCoachesAsync(CoachFilter filter)
        {
            List<CoachProfile> coaches = null;
            switch (filter.Game)
            {
                case Game.Dota:
                    coaches = await Set.Where(c => c.DotaProfile != null).Where(c => c.Language == filter.Language)
                        .ToListAsync();
                    break;
                case Game.Lol:
                    coaches = await Set.Where(c => c.LolProfile != null).Where(c => c.Language == filter.Language)
                        .ToListAsync();
                    break;
                case Game.Csgo:
                    coaches = await Set.Where(c => c.CsgoProfile != null).Where(c => c.Language == filter.Language)
                        .ToListAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return coaches?.Where(c => c.IsScheduleIntervalAvailable(new ScheduleInterval()
            {
                Start = filter.Start,
                Duration = filter.Duration,
            })).ToList();
        }
    }
}