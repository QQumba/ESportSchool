using ESportSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string email, CancellationToken ct = default);
        Task TopUpAsync(User user, decimal value, CancellationToken ct = default);
        Task WithdrawAsync(User user, decimal value, CancellationToken ct = default);
    }
}
