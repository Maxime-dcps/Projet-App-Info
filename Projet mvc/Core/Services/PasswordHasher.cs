using System.Security.Cryptography;

namespace Projet_mvc.Core.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int Iterations = 100000;
        private const int KeySize = 32;

        public (string hash, string salt) Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[SaltSize];
            rng.GetBytes(saltBytes);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(KeySize);

            string salt = Convert.ToBase64String(saltBytes);
            string hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashBytes = Convert.FromBase64String(hash);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            byte[] testHashBytes = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(hashBytes, testHashBytes);
        }
    }
}
