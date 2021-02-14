using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services
{
    public class PaymentService
    {
        private readonly IUserRepository _userRepository;

        public PaymentService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public void TopUpBalanceAsync(User user, decimal value)
        {
            _userRepository.TopUpAsync(user, value);
        }

        public void WithdrawAsync(User user, decimal value)
        {
            _userRepository.WithdrawAsync(user, value);
        }
    }
}