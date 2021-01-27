using System;
using ESportSchool.Domain.Entities;

namespace ESportSchool.Services
{
    public class PasswordVerificationService
    {
        public static string CreateHash(string password, out string salt)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password + salt);
        }

        public static bool VerifyPassword(string password, string salt, string passwordHash)
        {
            if (BCrypt.Net.BCrypt.Verify(password + salt, passwordHash))
            {
                return true;
            }
            Console.WriteLine("~HASH DO NOT MATCH~");
            return false;
        }
    }
}