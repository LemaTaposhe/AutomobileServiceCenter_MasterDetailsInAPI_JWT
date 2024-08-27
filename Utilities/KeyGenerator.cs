using System.Security.Cryptography;

namespace AutomobileServiceCenter_MasterDetailsInAPI.Utilities
{
    public static class KeyGenerator
    {
        public static string GenerateKey(int sizeInBytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[sizeInBytes];
                rng.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }
    }
}
