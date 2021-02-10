using ESportSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string email);
        void TopUp(User user, decimal value);
        void Withdraw(User user, decimal value);
    }
}
