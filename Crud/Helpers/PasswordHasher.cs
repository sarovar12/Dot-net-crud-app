using System.Security.Cryptography;
using System.Text;

namespace Crud.Helpers
{
    public class PasswordHasher
    {
        public static string ComputeHash(string password)
        {
            using var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(password);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            return hash;


        }


    }
}
