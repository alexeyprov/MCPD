using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomUI.Converters
{
    /// <summary>
    /// Compares two values and selects a brush based on the result.
    /// </summary>
    public class CompareResultToBrushConverter : IMultiValueConverter
    {
        #region Public Properties

        public Brush HighlightBrush
        {
            get;
            set;
        }

        public Brush DefaultBrush
        {
            get;
            set;
        }

        public CompareResult HighlightCriteria
        {
            get;
            set;
        }

        #endregion

        #region IMultiValueConverter Members

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                throw new ArgumentException(
                    string.Format(
                        "Can convert to {0} type only",
                        typeof(Brush)));
            }

            return (Compare(values) == HighlightCriteria) ?
                HighlightBrush :
                DefaultBrush;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation

        private CompareResult Compare(object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            if (values.Length != 2)
            {
                throw new ArgumentException("Exactly two values are required", "values");
            }

            IComparable first = values[0] as IComparable;
            IComparable second = values[1] as IComparable;

            if (first == null || second == null || first.GetType() != second.GetType())
            {
                return (CompareResult)(((int)HighlightCriteria + 1) % (int)CompareResult.Max);
            }

            int compareResult = first.CompareTo(second);

            if (compareResult == 0)
            {
                return CompareResult.Equals;
            }

            return (compareResult < 0) ?
                CompareResult.LessThan :
                CompareResult.MoreThan;
        }

        #endregion
    }
}
