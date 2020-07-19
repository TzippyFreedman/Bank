using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserService.Services
{
<<<<<<< HEAD
   public class Hash
    {
        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
        public static string CreateHash(string value, string salt)
=======
    public static class Hash
    {
        public static string CreatePasswordHash(string value, string salt)
>>>>>>> 68337d1ea66a061587107715c577d1e7f5747102
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool VerifyPassword(string value, string salt, string hash)
<<<<<<< HEAD
           => CreateHash(value, salt) == hash;
=======
            => CreatePasswordHash(value, salt) == hash;

        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

>>>>>>> 68337d1ea66a061587107715c577d1e7f5747102
    }
}
