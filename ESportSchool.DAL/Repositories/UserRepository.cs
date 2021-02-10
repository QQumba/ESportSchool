using ESportSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<User> GetAsync(string email)
        {
            return await Set.FirstOrDefaultAsync(u => u.Email == email);
        }


        public void TopUp(User user, decimal value)
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
            Update(user);
        }

        public void Withdraw(User user, decimal value)
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
            Update(user);
        }
    }
}
