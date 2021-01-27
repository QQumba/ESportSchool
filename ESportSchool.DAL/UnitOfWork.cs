using System.ComponentModel;
using ESportSchool.DAL.Repositories;
using ESportSchool.Domain;
using ESportSchool.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace ESportSchool.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ESportSchoolDBContext _db;
        
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;


        public UnitOfWork(ESportSchoolDBContext context)
        {
            _db = context;
        }

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_db));
        public ICommentRepository CommentRepository => _commentRepository ?? (_commentRepository = new CommentRepository(_db));
    }
}