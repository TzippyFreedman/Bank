using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string CreatePasswordHash(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);
            return Convert.ToBase64String(valueBytes);
        }
        public bool VerifyPassword(string value, string salt, string hash)
            => CreatePasswordHash(value, salt) == hash;
        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

    }
}
