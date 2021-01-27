using ESportSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESportSchool.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailOrDefaultAsync(string email);
        Task TopUpBalanceAsync(User user, float value);
        Task WithdrawAsync(User user, float value);
    }
}
