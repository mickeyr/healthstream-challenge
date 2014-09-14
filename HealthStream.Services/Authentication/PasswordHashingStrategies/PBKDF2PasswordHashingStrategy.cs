using System;
using System.Linq;
using System.Security.Cryptography;

namespace HealthStream.Services.Authentication.PasswordHashingStrategies
{
    internal class PBKDF2PasswordHashingStrategy : IPasswordHashingStrategy
    {
        public int HashLength { get; set; }
        public int SaltLength { get; set; }
        public int Iterations { get; set; }

        public PBKDF2PasswordHashingStrategy()
        {
            HashLength = 32;
            SaltLength = 32;
            Iterations = 1000;
        }

        public bool ValidatePasswordHash(string password, string hash)
        {
            var options = hash.Split(':');
            int iterations;
            if (!Int32.TryParse(options[0], out iterations))
            {
                throw new ArgumentException("Invalid password hash.", "hash");
            }
            Iterations = iterations;

            var salt = Convert.FromBase64String(options[1]);
            var correctHash = Convert.FromBase64String(options[2]);
            var calculatedHash = CalculateHash(password, salt, iterations, correctHash.Length);

            return correctHash.SequenceEqual(calculatedHash);
        }

        public string HashPassword(string password)
        {
            var salt = GenerateSalt(SaltLength);
            var passwordHash = CalculateHash(password, salt, Iterations, HashLength);
            return String.Format("{0}:{1}:{2}", 
                Iterations, 
                Convert.ToBase64String(salt),
                Convert.ToBase64String(passwordHash)
                );
        }

        private static byte[] GenerateSalt(int saltLength)
        {
            var rng = new RNGCryptoServiceProvider();
            var salt = new Byte[saltLength];
            rng.GetBytes(salt);

            return salt;
        }

        private static byte[] CalculateHash(string password, byte[] salt, int iterations, int hashLength)
        {
            var algorithm = new Rfc2898DeriveBytes(password, salt, iterations);
            return algorithm.GetBytes(hashLength);
        }
    }
}
