using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services
{
    public class PaymentService
    {
        private IUserRepository _userRepository;

        public PaymentService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task TopUpBalanceAsync(User user, float value)
        {
            await _userRepository.TopUpBalanceAsync(user, value);
        }

        public async Task WithdrawAsync(User user, float value)
        {
            await _userRepository.WithdrawAsync(user, value);
        }
    }
}