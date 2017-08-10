using System;
using System.Windows.Data;

namespace Cuboid.Shared.Converters
{
    public sealed class DoubleToTimespanConverter : IValueConverter
    {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;

            return timeSpan.TotalSeconds;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double totalSeconds = Convert.ToDouble(value);

            return TimeSpan.FromSeconds(totalSeconds);
        }

        #endregion
    }
}
