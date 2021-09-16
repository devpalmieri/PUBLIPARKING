using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Publisoftware.Utility
{
    public static class CryptMD5
    {
        public static string getMD5(string txt_to_crypt)
        {
            using (MD5 md5Hasher = MD5.Create())
            {
                byte[] hashedData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(txt_to_crypt));
                StringBuilder strBuilder = new StringBuilder();
                foreach (byte b in hashedData)
                {
                    strBuilder.Append(b.ToString("x2"));
                }

                return strBuilder.ToString();
            }
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = getMD5(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] getMD5OfFile(string p_filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(p_filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        public static bool VerifyMd5HashOfFile(string p_filename, byte[] p_hash)
        {
            byte[] hashOfInput = getMD5OfFile(p_filename);

            if (hashOfInput.SequenceEqual(p_hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string HashToString(byte[] p_hash)
        {
            string v_hash = "";
            foreach (byte v_byte in p_hash)
            {
                if (v_hash.Length == 0)
                {
                    v_hash = v_byte.ToString();
                }
                else
                {
                    v_hash = v_hash + " " + v_byte.ToString();
                }
            }

            return v_hash;
        }

        public static string Encrypt(string toEncrypt, string key = "chiave")
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key

            using (MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider())
            {
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();

                using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
                {
                    //set the secret key for the tripleDES algorithm
                    tdes.Key = keyArray;
                    //mode of operation. there are other 4 modes.
                    //We choose ECB(Electronic code Book)
                    tdes.Mode = CipherMode.ECB;
                    //padding mode(if any extra byte added)

                    tdes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform cTransform = tdes.CreateEncryptor())
                    {
                        //transform the specified region of bytes array to resultArray
                        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                        //Release resources held by TripleDes Encryptor
                        tdes.Clear();
                        //Return the encrypted data into unreadable string format
                        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipherString, string key = "chiave")
        {
            cipherString = cipherString.Replace(" ", "+");

            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //if hashing was used get the hash code with regards to your key
            using (MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider())
            {
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();

                using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
                {
                    //set the secret key for the tripleDES algorithm
                    tdes.Key = keyArray;
                    //mode of operation. there are other 4 modes. 
                    //We choose ECB(Electronic code Book)

                    tdes.Mode = CipherMode.ECB;
                    //padding mode(if any extra byte added)
                    tdes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform cTransform = tdes.CreateDecryptor())
                    {
                        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                        //Release resources held by TripleDes Encryptor                
                        tdes.Clear();
                        //return the Clear decrypted TEXT
                        return UTF8Encoding.UTF8.GetString(resultArray);
                    }
                }
            }
        }

    }
}
