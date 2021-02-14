using System;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services.Utils;

namespace ESportSchool.Services.DataAccess
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICoachRepository _coachRepository;

        public UserService(IUserRepository userRepository, ICoachRepository coachRepository)
        {
            _userRepository = userRepository;
            _coachRepository = coachRepository;
        }

        public async Task CreateUserAsync(User user, CancellationToken ct = default)
        {
            var hash = PasswordVerificationService.CreateHash(user.Password, out var salt);
            user.Password = hash;
            user.Salt = salt;

            user.Login = user.Email;
            
            await _userRepository.CreateAsync(user, ct);
        }

        public Task<User> GetUserAsync(int id, CancellationToken ct = default)
        {
            return _userRepository.GetAsync(id, ct);
        }

        public async Task<User> GetUserAsync(string email, CancellationToken ct = default)
        {
            return await _userRepository.GetAsync(email, ct);
        }

        public async Task<User> GetUserIfVerified(string email, string password, CancellationToken ct = default)
        {
            var user = await GetUserAsync(email, ct);
            if (user == null){return null;}

            if (PasswordVerificationService.VerifyPassword(password, user.Salt, user.Password))
            {
                return await Task.FromResult(user);
            }

            return await Task.FromResult<User>(null);
        }
        public async Task UpdateUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
            {
                user.Login = user.Email;
            }
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(User user, string confirmationCode = null, CancellationToken ct = default)
        {
            if (!user.HasConfirmedEmail) return;
            if (user.ConfirmationCode == confirmationCode)
            {
                await _userRepository.DeleteAsync(user, ct);
            }
            var codeGenerator = new ConfirmationCodeGenerator();
            var code = codeGenerator.GetCode();
            var sender = new EmailConfirmationSender(new EmailMessageBuilder(user));
            sender.SendMessage(user.Email, code);
        }

        public async Task CreateCoachProfileAsync(Coach profile, CancellationToken ct = default)
        {
            if (profile.User == null)
            {
                throw new Exception("Coach profile should be tied with user.");
            }
            await _coachRepository.CreateAsync(profile, ct);
        }
    }
}