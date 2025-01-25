using System.Text;
using FileEncryptor.ViewModels;
using Xunit;

public class EncryptionTests
{
    [Fact]
    public void EncryptionAndDecryptionTest()
    {
        var viewModel = new SymmetricPageViewModel();
        string keyword = "test";
        string text = "terstdtdttdtd";
        
        var result = viewModel.Encrypt(keyword, text);
        
        string decryptedText = viewModel.Decrypt(result.EncryptedData, keyword, result.IV);
        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public void TestIvEncoding()
    {
        var viewModel = new SymmetricPageViewModel();
        string keyword = "test";
        string text = "terstdtdttdtd";
        
        var result = viewModel.Encrypt(keyword, text);
        string ivAsString = result.IVToString();
        byte[] iv = Convert.FromBase64String(ivAsString);
        Assert.Equal(result.IV, iv);
    }
}