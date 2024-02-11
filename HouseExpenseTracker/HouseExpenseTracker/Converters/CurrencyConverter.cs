using HouseExpenseTracker.Extensions;
using System.Globalization;

namespace HouseExpenseTracker.Converters;

public class CurrencyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (string.IsNullOrEmpty(System.Convert.ToString(value)))
        {
            return string.Empty;
        }

        return System.Convert.ToSingle(value).ToCurrency();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
