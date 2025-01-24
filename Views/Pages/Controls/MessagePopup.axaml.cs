using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Tmds.DBus.Protocol;

namespace FileEncryptor.Views.Pages.Controls;

public partial class MessagePopup : UserControl
{
    public MessagePopup ()
    {
        InitializeComponent();
        
    }
    
    public MessageType Type
    {
        set => SetMessageType(value);
    }
    
    public string Message
    {
        get => MessageText.Text;
        set => MessageText.Text = value;
    }

    
    private void SetMessageType(MessageType type)
    {
        switch (type)
        {
            case MessageType.Info:
                MessageBoxBorder.Background = Brushes.LightBlue;
                MessageHeader.Text = "Info";
                break;
            case MessageType.Warning:
                MessageBoxBorder.Background = Brushes.LightGoldenrodYellow;
                MessageHeader.Text = "Warning";
                break;
            case MessageType.Error:
                MessageBoxBorder.Background = Brushes.LightCoral;
                MessageHeader.Text = "Error";
                break;
            case MessageType.Success:
                MessageBoxBorder.Background = Brushes.LightGreen;
                MessageHeader.Text = "Success";
                break;
        }
    }
}

public enum MessageType
{
    Info,
    Warning,
    Error,
    Success
}