using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Repositories
{
    public interface IScheduleIntervalRepository : IRepository<ScheduleInterval>
    {
        Task<List<ScheduleInterval>> GetOutdatedIntervalsAsync();
    }
}