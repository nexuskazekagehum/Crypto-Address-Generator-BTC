using System.Security.Cryptography;
using System.Text;

namespace CryptoAddressGeneratorBTC.Core.Security
{
    public interface ITokenGenerator
    {
        string GenerateToken(int length = 32);
        string GenerateApiKey(string prefix = "ak");
        string GenerateCorrelationId();
    }

    public class SecureTokenGenerator : ITokenGenerator
    {
        public string GenerateToken(int length = 32)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public string GenerateApiKey(string prefix = "ak")
        {
            return $"{prefix}_{GenerateToken(24)}_{RandomNumberGenerator.GetInt32(1000, 9999)}";
        }

        public string GenerateCorrelationId()
        {
            var guid = Guid.NewGuid().ToString("N");
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(guid));
            return Convert.ToHexString(hash)[..16].ToLowerInvariant();
        }
    }
}
