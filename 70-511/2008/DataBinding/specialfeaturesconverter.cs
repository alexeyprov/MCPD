using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace DataBindingLab
{
    //<Snippet3>
    class SpecialFeaturesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			int rating;
			DateTime date;
			try
			{
				rating = (int)values[0];
				date = (DateTime)values[1];
			}
			catch (InvalidCastException)
			{
				return false; //fixing design-time error
			}

            // if the user has a good rating (10+) and has been a member for more than a year, special features are available
            if((rating >= 10) && (date.Date < (DateTime.Now.Date - new TimeSpan(365, 0, 0, 0))))
            {
                return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[2] { Binding.DoNothing, Binding.DoNothing };
        }
    }
    //</Snippet3>
}
