using System;
using Avalonia.Data.Converters;

namespace CookinGest.src.Converters;
public class DecimalToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is decimal dec)
        {
            return dec.ToString("0", culture);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (decimal.TryParse(value?.ToString(), out decimal dec))
        {
            return dec;
        }

        return 0m;
    }
}