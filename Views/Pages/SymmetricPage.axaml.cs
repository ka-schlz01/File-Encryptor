using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GetStartedApp.Models.Interfaces;
using GetStartedApp.Utils;

namespace FileEncryptor.Views;

public partial class SymmetricPage : UserControl, IEncryption
{
    public SymmetricPage()
    {
        InitializeComponent();
    }

    public void Encrypt()
    {
        
    }
    
    public void Decrypt()
    {
        
    }
    
    public void OnClick(object source, RoutedEventArgs args)
    {
        byte[] key = Aes256Helper.GenerateRandomKey();
        byte[] iv = Aes256Helper.GenerateRandomIV();
        
        // Encrypt the string
        string encrypted = Aes256Helper.Encrypt(Text.Text, key, iv);
        Console.WriteLine($"Encrypted: {encrypted}");
        
        // Decrypt the string
        string decrypted = Aes256Helper.Decrypt(encrypted, key, iv);
        Console.WriteLine($"Decrypted: {decrypted}");
    }
}