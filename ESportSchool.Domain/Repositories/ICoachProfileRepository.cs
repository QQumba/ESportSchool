using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Repositories
{
    public interface ICoachProfileRepository : IRepository<CoachProfile>
    {
        Task<List<CoachProfile>> GetAvailableCoachesAsync(CoachFilter filter);
    }
}