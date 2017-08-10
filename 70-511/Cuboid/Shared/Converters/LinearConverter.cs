using System;
using System.Windows.Data;

namespace Cuboid.Shared.Converters
{
    public class LinearConverter : IValueConverter
    {
        public double Alpha
        {
            get;
            set;
        }

        public double Beta
        {
            get;
            set;
        }

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double x = Convert.ToDouble(value);

            return Alpha * x + Beta;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
