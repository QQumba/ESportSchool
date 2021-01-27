using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ESportSchool.Domain;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services.Utils;
using Microsoft.IdentityModel.Tokens;

namespace ESportSchool.Services
{
    public class UserService
    {
        private readonly IUserRepository _ur;
        private readonly ICoachProfileRepository _coachProfileRepository;

        public UserService(IUserRepository userRepository, ICoachProfileRepository coachProfileRepository)
        {
            _ur = userRepository;
            _coachProfileRepository = coachProfileRepository;
        }

        public async Task CreateNewUserAsync(User user)
        {
            var hash = PasswordVerificationService.CreateHash(user.Password, out var salt);
            user.Password = hash;
            user.Salt = salt;
            await _ur.AddAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _ur.GetByEmailOrDefaultAsync(email);
        }

        public async Task<User> GetUserIfVerified(string email, string password)
        {
            var user = await GetUserAsync(email);
            if (user == null){return null;}

            Console.WriteLine($"~USER_EMAIL~ {user.Email}");
            Console.WriteLine($"~USER_PASS~ {user.Password}");
            Console.WriteLine($"~PROVIDED_PASS~");
            Console.WriteLine($"~USER_SALT~ {user.Salt}");
            
            if (PasswordVerificationService.VerifyPassword(password, user.Salt, user.Password))
            {
                Console.WriteLine("~VERIFIED~");
                return await Task.FromResult(user);
            }

            return await Task.FromResult<User>(null);
        }


        public async Task UpdateUserAsync(User user)
        {
            await _ur.UpdateAsync(user);
        }

        public void DeleteUser(User user, string confirmationCode = null)
        {
            if (!user.HasConfirmedEmail) return;
            if (user.ConfirmationCode == confirmationCode)
            {
                _ur.Remove(user);
            }
            var codeGenerator = new ConfirmationCodeGenerator();
            var code = codeGenerator.GetCode();
            var sender = new EmailConfirmationSender(new EmailMessageBuilder(user));
            sender.SendMessage(user.Email, code);
        }

        public async Task CreateCoachAccountAsync(CoachProfile profile)
        {
            if (profile.User == null)
            {
                throw new Exception("Coach profile should be tied with user.");
            }
            await _coachProfileRepository.AddAsync(profile);
        }
    }
}