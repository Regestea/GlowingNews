using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security.Shared.Extensions
{
    public static class TokenGenerator
    {
        public static string GenerateNewRngCrypto()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[50];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
