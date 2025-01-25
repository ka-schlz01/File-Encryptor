using System;

namespace GetStartedApp.Models.Interfaces;

public class SymmetricEncryptionResult
{
    public byte[] EncryptedData { get; set; }
    public byte[] IV { get; set; }

    public string EncryptedDataToString()
    {
        return Convert.ToBase64String(this.EncryptedData);
    }
    
    public string IVToString()
    {
        return Convert.ToBase64String(this.IV);
    }
    
    
}