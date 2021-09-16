using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Utility.Crypto
{
    public static class CryptSymmetric
    {
        private static readonly string password = "Publisoftware@2015";

        public static int keyLength = 128;

        public static SymmetricAlgorithm InitSymmetric(SymmetricAlgorithm algorithm, string password, int keyBitLength)
        {
            var salt = new byte[] { 1, 2, 23, 234, 37, 48, 134, 63, 248, 4 };

            const int Iterations = 234;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                if (!algorithm.ValidKeySize(keyBitLength))
                    throw new InvalidOperationException("Invalid size key");

                algorithm.Key = rfc2898DeriveBytes.GetBytes(keyBitLength / 8);
                algorithm.IV = rfc2898DeriveBytes.GetBytes(algorithm.BlockSize / 8);
                return algorithm;
            }
        }

        private static Aes initCryptObj()
        {
            SymmetricAlgorithm rjm = Aes.Create();
            rjm = InitSymmetric(rjm, password, keyLength);
            return (Aes)rjm;
        }

        public static string Encode(string testo)
        {
            using (Aes rjm = initCryptObj())
            {
                byte[] input = Encoding.UTF8.GetBytes(testo);
                byte[] output = rjm.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);

                return Convert.ToBase64String(output);
            }
        }

        public static string Decode(string testo)
        {
            using (Aes rjm = initCryptObj())
            {
                byte[] input = Convert.FromBase64String(testo);
                byte[] output = rjm.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);

                return Encoding.UTF8.GetString(output);
            }
        }
    }
}
