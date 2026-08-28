using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;

namespace projetobiblioteca.Tools.Bearer.Implementation
{
    public class PasswordHashService : 
        IPasswordHasherService
    {
        public string Hash(string password)
        {
            var inputBytes = Encoding
                .UTF8.GetBytes(password);
            var hashedBytes = SHA256
                .HashData(inputBytes);
            var builder = new StringBuilder();
            foreach ( var b in hashedBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public bool Verify(string password,
            string hashedPassword)
        {
            var hashOfInput = Hash(password);
            return string.Equals(hashOfInput,
                hashedPassword,
                StringComparison
                .OrdinalIgnoreCase);
        }
    }
}
