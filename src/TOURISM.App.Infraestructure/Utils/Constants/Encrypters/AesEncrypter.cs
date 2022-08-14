using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TOURISM.App.Infrastructure.Utils.Constants.Encrypters
{
    public static class AesEncrypter
    {
        public static string EncryptedUrlBase64(string text, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(iv))
                return string.Empty;

            var urlEncode = HttpUtility.UrlEncode(EncryptedBase64(text, key, iv));
            return urlEncode;
        }

        public static string DecryptedUrlBase64(string text, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(iv))
                return string.Empty;

            var urlDecode = DecryptedBase64(HttpUtility.UrlDecode(text), key, iv);
            return urlDecode;
        }

        public static string EncryptedBase64(string text, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(iv))
                return string.Empty;

            var _key = Convert.FromBase64String(key);
            var _IV = Convert.FromBase64String(iv);

            var encrypted = Encrypt(text, _key, _IV);
            var encryptedBase64 = Convert.ToBase64String(encrypted);
            return encryptedBase64;
        }

        public static string DecryptedBase64(string text, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(iv))
                return string.Empty;

            var _key = Convert.FromBase64String(key);
            var _IV = Convert.FromBase64String(iv);

            var urlDecode = text;
            var decryptedBase64 = Convert.FromBase64String(urlDecode);
            var decrypted = Decrypt(decryptedBase64, _key, _IV);

            return decrypted;
        }

        #region Utils
        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;  
            using (var aes = new AesManaged())
            {   
                var encryptor = aes.CreateEncryptor(Key, IV);  
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    { 
                        using (var sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }  
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            var plaintext = string.Empty;
            using (var aes = new AesManaged())
            {  
                var decryptor = aes.CreateDecryptor(Key, IV); 
                using (var ms = new MemoryStream(cipherText))
                {   
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        #endregion
    }
}
