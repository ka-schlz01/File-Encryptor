using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace FileEncryptor.Views.Pages;

public partial class StartScreen : UserControl
{
    private Action<UserControl> ChangeMainContentAction;
    
    public StartScreen(Action<UserControl> changeMainContentAction)
    {
        InitializeComponent();

        ChangeMainContentAction = changeMainContentAction;
    }
    
    private void SymmetricButton_Click(object? sender, RoutedEventArgs e)
    {
        ChangeMainContentAction.Invoke(new SymmetricPage());
    }

    private void AES256Button_Click(object? sender, RoutedEventArgs e)
    {
        ChangeMainContentAction.Invoke(new AESPage());
    }
}