using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class GameProfileRepository : Repository<GameProfile>, IGameProfileRepository
    {
        public GameProfileRepository(ESportSchoolDbContext context) : base(context)
        {
        }
    }
}