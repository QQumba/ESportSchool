using ESportSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ESportSchoolDBContext context)
            : base(context)
        {
        }

        public async Task<User> GetByEmailOrDefaultAsync(string email)
        {
            return await Set.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task TopUpBalanceAsync(User user, float value)
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
            await UpdateAsync(user);
        }

        public async Task WithdrawAsync(User user, float value)
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
            await UpdateAsync(user);
        }
    }
}
