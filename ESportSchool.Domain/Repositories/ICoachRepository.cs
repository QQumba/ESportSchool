using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Repositories
{
    public interface ICoachRepository : IRepository<Coach>
    {
        Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter, CancellationToken ct = default);
    }
}