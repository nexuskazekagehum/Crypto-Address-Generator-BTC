using System.Security.Cryptography;
using System.Text;

namespace CryptoAddressGeneratorBTC.Core.Security
{
    public static class HashUtils
    {
        public static string Sha256Hex(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        public static string HmacSha256Hex(string input, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using var hmac = new HMACSHA256(keyBytes);
            var hash = hmac.ComputeHash(inputBytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        public static string GenerateNonce(int length = 16)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
