using System;
using System.Security.Cryptography;
using System.Text;

namespace PassBruteForce
{
    public class PasswordHandler
    {
        private static readonly string Salt = "crackSalt";
        public string EncodedPassword { get; private set; } = string.Empty;

        public void ComputeHash(string userPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var saltedPassword = userPassword + Salt;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                EncodedPassword = Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
