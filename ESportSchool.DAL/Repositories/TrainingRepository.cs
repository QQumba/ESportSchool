using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        public TrainingRepository(ESportSchoolDBContext context) : base(context) { }
    }
}