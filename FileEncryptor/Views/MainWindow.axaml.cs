using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FileEncryptor.Views;
using FileEncryptor.Views.Pages;
using GetStartedApp.Utils;

namespace GetStartedApp.Views;

public partial class MainWindow : Window
{
    private readonly Stack<UserControl> _navigationStack = new();
    
    public MainWindow()
    {
        InitializeComponent();
        Navigate(new StartScreen(Navigate));
    }
    
    public void Navigate(UserControl newPage)
    {
        if (MainContent.Content is UserControl currentPage)
        {
            _navigationStack.Push(currentPage);
        }
        
        MainContent.Content = newPage;
        
        BackButton.IsVisible = _navigationStack.Count > 0;
    }

    private void OnBackButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_navigationStack.Count > 0)
        {
            // Navigate to the previous page
            var previousPage = _navigationStack.Pop();
            MainContent.Content = previousPage;

            // Update back button visibility
            BackButton.IsVisible = _navigationStack.Count > 0;
        }
    }

    private void ChangeMainContent(UserControl obj)
    {
        MainContent.Content = obj;
    }
    
    
    
}