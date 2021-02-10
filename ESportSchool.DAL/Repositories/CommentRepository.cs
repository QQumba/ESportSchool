using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>,ICommentRepository
    {
        public CommentRepository(ESportSchoolDbContext context) : base(context)
        {
        }
    }
}