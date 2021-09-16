using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Utility.Crypto
{
    // ref: https://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt
    public class CryptAES
    {
        static readonly byte[] defaultSalt = new byte[] { 10, 1, 34, 17, 10, 1, 1, 203 };

        public static int DefaultSaltLength { get { return 8; } }
        public const int Rfc2898iterationCount = 1000;

        public static byte[] GetRandomBytes()
        {
            byte[] ba = new byte[DefaultSaltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            if (rfc2898SaltBytes_MoreThanEight == null)
            {
                rfc2898SaltBytes_MoreThanEight = defaultSalt;
            }
            if (rfc2898SaltBytes_MoreThanEight.Length < 8)
            {
                throw new ArgumentException($"{nameof(rfc2898SaltBytes_MoreThanEight)} deve essere di almeno 8 byte");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, rfc2898SaltBytes_MoreThanEight, Rfc2898iterationCount);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            if (rfc2898SaltBytes_MoreThanEight == null)
            {
                rfc2898SaltBytes_MoreThanEight = defaultSalt;
            }
            if (rfc2898SaltBytes_MoreThanEight.Length < 8)
            {
                throw new ArgumentException($"{nameof(rfc2898SaltBytes_MoreThanEight)} deve essere di almeno 8 byte");
            }


            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, rfc2898SaltBytes_MoreThanEight, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static string EncryptText(string input, string password, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var sha = SHA256.Create())
            {
                // Hash the password with SHA256
                passwordBytes = sha.ComputeHash(passwordBytes);

                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes, rfc2898SaltBytes_MoreThanEight);
                string result = Convert.ToBase64String(bytesEncrypted);
                return result;
            }
        }

        public string DecryptText(string input, string password, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (var sha = SHA256.Create())
            {
                passwordBytes = sha.ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes, rfc2898SaltBytes_MoreThanEight);
                string result = Encoding.UTF8.GetString(bytesDecrypted);
                return result;
            }
        }

        /// <summary>
        /// Crittografa una stringa usando un salt pseudo-random intestato alla stringa crittografata
        /// </summary>
        /// <param name="text">Stringa da crittografare</param>
        /// <param name="password">Password per criptare</param>
        /// <param name="rfc2898SaltBytes_MoreThanEight">Il salt utilizzato dalla fnz Rfc2898DeriveBytes, non il salt random</param>
        /// <returns>stringa crittografata</returns>
        /// <remarks>Il salt pseudo-random non è <paramref name="rfc2898SaltBytes_MoreThanEight"/> </remarks>
        public static string EncryptStringWithRndSalt(string text, string password, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(text);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
            {
                baEncrypted[i + baSalt.Length] = baText[i];
            }
            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash, null);

            string result = Convert.ToBase64String(baEncrypted);
            return result;
        }
        /// <summary>
        /// Decripta una stringa criptata utilizzando un salt pseudo-random intestato alla stringa crittografata
        /// </summary>
        /// <param name="text">Stringa da decriptare</param>
        /// <param name="password">Password per decrittare la stringa</param>
        /// <param name="rfc2898SaltBytes_MoreThanEight">Il salt utilizzato dalla fnz Rfc2898DeriveBytes, non il salt random. Deve essere lo stesso utilizzato in face di criptaggio</param>
        /// <returns>stringa crittografata</returns>
        /// <remarks>Il salt pseudo-random non è <paramref name="rfc2898SaltBytes_MoreThanEight"/> </remarks>
        public static string DecryptStringRndSalt(string text, string password, byte[] rfc2898SaltBytes_MoreThanEight = null)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(password);
            // Hash the password with SHA256
            using (var sha = SHA256Managed.Create())
            {
                byte[] baPwdHash = sha.ComputeHash(baPwd);
                byte[] baText = Convert.FromBase64String(text);
                byte[] baDecrypted = AES_Decrypt(baText, baPwdHash, null);

                // Remove salt
                int saltLength = DefaultSaltLength;
                byte[] baResult = new byte[baDecrypted.Length - saltLength];
                for (int i = 0; i < baResult.Length; i++)
                {
                    baResult[i] = baDecrypted[i + saltLength];
                }
                string result = Encoding.UTF8.GetString(baResult);
                return result;
            }
        }

        public static void EncryptFile(string unencryptedFilePathInput, string encryptedfilePathOutput, string password, byte[] saltBytes_MoreThanEight)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(unencryptedFilePathInput);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var sha = SHA256.Create())
            {
                // Hash the password with SHA256
                passwordBytes = sha.ComputeHash(passwordBytes);
                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes, saltBytes_MoreThanEight);
                File.WriteAllBytes(encryptedfilePathOutput, bytesEncrypted);
            }
        }

        public static void DecryptFile(string encryptedfilePathInput, string unencryptedFilePathoutput, string password, byte[] saltBytes_MoreThanEight)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(encryptedfilePathInput);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var sha = SHA256.Create())
            {
                passwordBytes = sha.ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes, saltBytes_MoreThanEight);
                File.WriteAllBytes(unencryptedFilePathoutput, bytesDecrypted);
            }
        }
    }
}
