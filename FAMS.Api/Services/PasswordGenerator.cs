using FAMS.Core.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace FAMS.Api.Services
{
    public class PasswordGenerator: IPasswordGenerator
    {
        public string GeneratePassword(int length)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                StringBuilder password = new StringBuilder(length);

                foreach (byte b in randomBytes)
                {
                    password.Append(allowedChars[b % allowedChars.Length]);
                }

                return password.ToString();
            }
        }
    }
}
