using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using GetStartedApp.Models;

namespace GetStartedApp.Managers;

public class EncryptionManager
{
    public static void EncryptAndSave(string filePath, byte[] key, byte[] iv)
    {
        byte[] encryptedKey = ProtectedData.Protect(key, null, DataProtectionScope.CurrentUser);
        byte[] encryptedIV = ProtectedData.Protect(iv, null, DataProtectionScope.CurrentUser);
        
        string encryptedKeyBase64 = Convert.ToBase64String(encryptedKey);
        string encryptedIVBase64 = Convert.ToBase64String(encryptedIV);
        
        string jsonConfig = $@"
        {{
            ""EncryptionKey"": ""{encryptedKeyBase64}"",
            ""EncryptionIV"": ""{encryptedIVBase64}""
        }}";

        File.WriteAllText(filePath, jsonConfig);
    }
    
    public static (byte[] Key, byte[] IV) LoadAndDecrypt(string filePath)
    {
        string jsonConfig = File.ReadAllText(filePath);
        var config = JsonSerializer.Deserialize<EncryptionFileConfig>(jsonConfig);
        
        byte[] encryptedKey = Convert.FromBase64String(config.EncryptionKey);
        byte[] encryptedIV = Convert.FromBase64String(config.EncryptionIV);

        byte[] key = ProtectedData.Unprotect(encryptedKey, null, DataProtectionScope.CurrentUser);
        byte[] iv = ProtectedData.Unprotect(encryptedIV, null, DataProtectionScope.CurrentUser);

        return (key, iv);
    }
}