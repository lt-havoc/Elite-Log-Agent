#nullable enable
namespace DW.ELA.Controller.Converters;

using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ViewModels;

public class ValidityToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && Enum.TryParse(value.ToString(), out ApiKeyValidity validity))
        {
            return validity switch
            {
                ApiKeyValidity.NotChecked => Brush.Parse("#000000"),
                ApiKeyValidity.Checking => Brush.Parse("#000000"),
                ApiKeyValidity.Unknown => Brush.Parse("#FFC107"),
                ApiKeyValidity.Invalid => Brush.Parse("#DC3545"),
                ApiKeyValidity.Valid => Brush.Parse("#28A745"),
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        throw new ArgumentOutOfRangeException(nameof(value));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
}