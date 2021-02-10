using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class GuideRepository : Repository<Guide>, IGuideRepository
    {
        public GuideRepository(ESportSchoolDbContext context) : base(context)
        {
        }
    }
}