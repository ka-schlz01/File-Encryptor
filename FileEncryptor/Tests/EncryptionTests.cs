using FileEncryptor.ViewModels;
using Xunit;

namespace GetStartedApp.Tests;

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
}