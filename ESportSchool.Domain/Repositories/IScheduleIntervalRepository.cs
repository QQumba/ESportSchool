using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;

namespace ESportSchool.Domain.Repositories
{
    public interface IScheduleIntervalRepository : IRepository<ScheduleInterval>
    {
        Task DeleteRangeAsync(IEnumerable<ScheduleInterval> intervals);
        Task<List<ScheduleInterval>> GetOutdatedIntervalsAsync();
    }
}