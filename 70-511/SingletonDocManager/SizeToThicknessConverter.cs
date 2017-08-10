using System;
using System.Windows;
using System.Windows.Data;

namespace SingletonDocManager
{
    internal sealed class SizeToThicknessConverter : IValueConverter
    {
        #region Public Properties

        public double Left
        {
            get;
            set;
        }

        public double Top
        {
            get;
            set;
        }

        #endregion

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Thickness))
            {
                throw new ArgumentException(
                    string.Format(
                        "Cannot convert to {0} type",
                        targetType),
                    "targetType");
            }

            Size size = (Size)value;

            return new Thickness(Left, Top, Left + size.Width, Top + size.Height);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
