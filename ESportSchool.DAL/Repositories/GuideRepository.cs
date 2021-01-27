using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class GuideRepository : Repository<Guide>, IGuideRepository
    {
        public GuideRepository(ESportSchoolDBContext context) : base(context)
        {
        }
    }
}