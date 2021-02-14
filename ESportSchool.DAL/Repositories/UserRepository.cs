using ESportSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ESportSchoolDbContext context)
            : base(context)
        {
        }

        public async Task<User> GetAsync(string email, CancellationToken ct = default)
        {
            return await Set.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: ct);
        }


        public async Task TopUpAsync(User user, decimal value, CancellationToken ct)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "should be above zero");
            }

            if (!Set.Contains(user))
            {
                throw new ArgumentException(nameof(user), user.Email);
            }

            user.Balance += value;
            await UpdateAsync(user, ct);
        }

        public async Task WithdrawAsync(User user, decimal value, CancellationToken ct)
        {
            if (user.Balance - value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "should be above zero");
            }

            if (!Set.Contains(user))
            {
                throw new ArgumentException(nameof(user), user.Email);
            }

            user.Balance -= value;
            await UpdateAsync(user, ct);
        }
    }
}
