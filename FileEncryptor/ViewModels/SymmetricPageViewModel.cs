using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models.Interfaces;
using GetStartedApp.ViewModels;

namespace FileEncryptor.ViewModels;



public partial class SymmetricPageViewModel : ViewModelBase
{

    [ObservableProperty]
    private bool _isToggled;
    
    
    public SymmetricEncryptionResult Encrypt(string keyword, string textToEncode)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKeyFromKeyword(keyword);
            aes.GenerateIV(); 

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(textToEncode);
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();

                return new SymmetricEncryptionResult {EncryptedData =  ms.ToArray(), IV = aes.IV};
            }
        }
    }
    
    private static byte[] GetKeyFromKeyword(string keyword)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(keyword));
        }
    }
    
    public string? Decrypt(byte[] encryptedData, string keyword, byte[] iv)
    {
        try
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = GetKeyFromKeyword(keyword);
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedData, 0, encryptedData.Length);
                    cs.FlushFinalBlock();

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        catch (Exception e)
        {
            Console.Error.Write(e.Message);
            return null;
        }
    }

    

}

    
