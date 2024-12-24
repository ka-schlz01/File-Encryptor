using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GetStartedApp.Utils;

public class Aes256Helper
{
    public static string Encrypt(string plainText, byte[] key, byte[] iv)
    {
        if (key.Length != 32) throw new ArgumentException("Key must be 256 bits (32 bytes).");
        if (iv.Length != 16) throw new ArgumentException("IV must be 128 bits (16 bytes).");

        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
    
    public static string Decrypt(string cipherText, byte[] key, byte[] iv)
    {
        if (key.Length != 32) throw new ArgumentException("Key must be 256 bits (32 bytes).");
        if (iv.Length != 16) throw new ArgumentException("IV must be 128 bits (16 bytes).");

        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                byte[] plainBytes = new byte[cipherText.Length];
                int bytesRead = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                return Encoding.UTF8.GetString(plainBytes, 0, bytesRead);
            }
        }
    }
    
    public static byte[] GenerateRandomKey()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] key = new byte[32];
            rng.GetBytes(key);
            return key;
        }
    }
    
    public static byte[] GenerateRandomIV()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            return iv;
        }
    }
}