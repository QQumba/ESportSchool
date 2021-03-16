using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter, CancellationToken ct = default)
        {
            return Set
                .Where(c => c.GameProfiles.FirstOrDefault(p => p.Game == filter.Game) != null)
                .Where(c => c.Languages.Contains(filter.Language))
                .Where(c => c.IsScheduleIntervalAvailable(
                    new ScheduleInterval()
                    {
                        Start = filter.Start,
                        Duration = filter.Duration,
                    }))
                .ToListAsync(cancellationToken: ct);
        }
    }
}