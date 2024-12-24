using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GetStartedApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void ButtonClicked(object source, RoutedEventArgs args)
    {
        Console.WriteLine("Click!! Celsius: " + Celsius.Text);
    }
    
}