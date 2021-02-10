using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public class ScheduleIntervalRepository : Repository<ScheduleInterval>, IScheduleIntervalRepository
    {
        public ScheduleIntervalRepository(ESportSchoolDbContext context) : base(context)
        {
        }

        public async Task<List<ScheduleInterval>> GetOutdatedIntervalsAsync()
        {
            return await Set.Where(i => i.RepeatWeekly == false && i.Start < DateTime.Now).ToListAsync();
        }
    }
}