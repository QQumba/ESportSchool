using ESportSchool.Domain.Repositories;

namespace ESportSchool.Domain
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICommentRepository CommentRepository { get; }
    }
}