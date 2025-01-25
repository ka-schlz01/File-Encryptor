using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FileEncryptor.ViewModels;
using FileEncryptor.Views.Pages.Controls;
using GetStartedApp.Models.Interfaces;
using GetStartedApp.Utils;

namespace FileEncryptor.Views;

public partial class SymmetricPage : UserControl
{
    
    private readonly SymmetricPageViewModel _viewModel = new();
    
    public SymmetricPage()
    {
        InitializeComponent();
        
        DataContext = _viewModel;
    }
    public void OnClick(object source, RoutedEventArgs args)
    {
        string? text = Text.Text;
        string? keyword = Keyword.Text;
        
        if (text != null && keyword != null)
        {
            if (Switch.IsChecked.GetValueOrDefault())
            {
                string? iv = IV.Text;
                if (iv == null)
                {
                    ShowMessageBox(MessageType.Error, "Missing IV!"); 
                    return;
                }
                
                string result = _viewModel.Decrypt(
                    Encoding.UTF8.GetBytes(text),
                    keyword, 
                    Encoding.UTF8.GetBytes(iv)
                    );
                if (result == null)
                {
                    ShowMessageBox(MessageType.Error, "Decryption error!"); 
                    return;
                }
                EncryptionTextPopup.IsOpen = true;
                EncryptedText.Text = result;
                return;

            }
            
            EncryptionTextPopup.IsOpen = true;
            SymmetricEncryptionResult encryptionResult = _viewModel.Encrypt(keyword, text);
            EncryptedText.Text = encryptionResult.EncryptedDataToString();
            EncryptionIV.Text = encryptionResult.IVToString();
        }
        else
        {
            ShowMessageBox(MessageType.Error, "Missing text or keyword!");
        }
    }
    
    private void ShowMessageBox(MessageType type, string message)
    {
        MessagePopupControl.Type = type;
        MessagePopupControl.Message = message;
        PopupMessage.IsOpen = true;
    }

    public async void CopyEncryptedText(object source, RoutedEventArgs args)
    {
        await CopyToClipboardAsync(EncryptedText.Text);

    }
    
    public async void CopyIV(object source, RoutedEventArgs args)
    {
        await CopyToClipboardAsync(EncryptionIV.Text);
        
    }
    
    private async Task CopyToClipboardAsync(string text)
    {
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

        if (clipboard != null)
        {
            await clipboard.SetTextAsync(text);
        }
    }
    


    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        EncryptionTextPopup.IsOpen = false;
    }
}