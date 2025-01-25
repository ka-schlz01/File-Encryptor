using System;
using Avalonia.Data.Converters;

namespace GetStartedApp.Converters;

public class BoolToTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool isToggled)
        {
            return isToggled ? "Decrypt" : "Encrypt";
        }
        return "Default";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}