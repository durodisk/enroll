using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ENROLL.Helpers
{
    public static class StringCipher
    {
        private const int Keysize = 256;

        private const int DerivationIterations = 1000;

        public static string Decrypt(string cipherText, string passPhrase)
        {
            string str;
            try
            {
                byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take<byte>(32).ToArray<byte>();
                byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip<byte>(32).Take<byte>(32).ToArray<byte>();
                byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip<byte>(64).Take<byte>((int)cipherTextBytesWithSaltAndIv.Length - 64).ToArray<byte>();
                using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, 1000))
                {
                    byte[] keyBytes = password.GetBytes(32);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[(int)cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, (int)plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    str = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                str = string.Empty;
            }
            return str;
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            string base64String;
            byte[] saltStringBytes = StringCipher.Generate256BitsOfRandomEntropy();
            byte[] ivStringBytes = StringCipher.Generate256BitsOfRandomEntropy();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, 1000))
            {
                byte[] keyBytes = password.GetBytes(32);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, (int)plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = saltStringBytes.Concat<byte>(ivStringBytes).ToArray<byte>();
                                cipherTextBytes = cipherTextBytes.Concat<byte>(memoryStream.ToArray()).ToArray<byte>();
                                memoryStream.Close();
                                cryptoStream.Close();
                                base64String = Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
            return base64String;
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}