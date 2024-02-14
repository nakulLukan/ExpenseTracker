using System.Globalization;

namespace HouseExpenseTracker.Converters;

public class PickTopErrorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return string.Empty;
        }
        if (value is not IEnumerable<string> errors)
        {
            throw new ApplicationException();
        }

        return errors.FirstOrDefault() ?? string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
