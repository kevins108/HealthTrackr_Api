using HealthTrackr_Api.Data;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace HealthTrackr_Api.Services
{
    public class UserLoginAccess
    {
        public ApplicationSettings _settings { get; }

        public UserLoginAccess(IOptionsSnapshot<ApplicationSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Encrypt(string? password)
        {
            if (!string.IsNullOrEmpty(_settings.ENCRYPTION_KEY) && !string.IsNullOrEmpty(password))
            {
                var desCryptoProvider = TripleDES.Create();
                var byteHash = MD5.HashData(Encoding.UTF8.GetBytes(_settings.ENCRYPTION_KEY));
                var byteBuff = Encoding.UTF8.GetBytes(password);
                desCryptoProvider.Key = byteHash;
                desCryptoProvider.Mode = CipherMode.ECB;
                return Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            return password ?? string.Empty;
        }

        public string Decrypt(string? encryptedPassword)
        {
            if (!string.IsNullOrEmpty(_settings.ENCRYPTION_KEY) && !string.IsNullOrEmpty(encryptedPassword))
            {
                var desCryptoProvider = TripleDES.Create();
                var byteHash = MD5.HashData(Encoding.UTF8.GetBytes(_settings.ENCRYPTION_KEY));
                var byteBuff = Convert.FromBase64String(encryptedPassword);
                desCryptoProvider.Key = byteHash;
                desCryptoProvider.Mode = CipherMode.ECB;
                return Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            return encryptedPassword ?? string.Empty;
        }

    }
}
