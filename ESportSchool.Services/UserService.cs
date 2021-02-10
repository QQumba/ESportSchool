using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ESportSchool.Domain;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services.Utils;
using Microsoft.IdentityModel.Tokens;

namespace ESportSchool.Services
{
    public class UserService
    {
        private readonly IUserRepository _ur;
        private readonly ICoachRepository _coachRepository;

        public UserService(IUserRepository userRepository, ICoachRepository coachRepository)
        {
            _ur = userRepository;
            _coachRepository = coachRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            var hash = PasswordVerificationService.CreateHash(user.Password, out var salt);
            user.Password = hash;
            user.Salt = salt;
            await _ur.CreateAsync(user);
            await _ur.SaveChangesAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return _ur.GetAsync(id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _ur.GetAsync(email);
        }

        public async Task<User> GetUserIfVerified(string email, string password)
        {
            var user = await GetUserAsync(email);
            if (user == null){return null;}

            if (PasswordVerificationService.VerifyPassword(password, user.Salt, user.Password))
            {
                return await Task.FromResult(user);
            }

            return await Task.FromResult<User>(null);
        }
        public async Task UpdateUserAsync(User user)
        {
            _ur.Update(user);
            await _ur.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user, string confirmationCode = null)
        {
            if (!user.HasConfirmedEmail) return;
            if (user.ConfirmationCode == confirmationCode)
            {
                _ur.Delete(user);
                await _ur.SaveChangesAsync();
            }
            var codeGenerator = new ConfirmationCodeGenerator();
            var code = codeGenerator.GetCode();
            var sender = new EmailConfirmationSender(new EmailMessageBuilder(user));
            sender.SendMessage(user.Email, code);
        }

        public async Task CreateCoachAccountAsync(Coach profile)
        {
            if (profile.User == null)
            {
                throw new Exception("Coach profile should be tied with user.");
            }
            await _coachRepository.CreateAsync(profile);
        }
    }
}