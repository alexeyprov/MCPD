using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace CustomUI.Interop
{
    public static class NotifyPropertyChangedExtensions
    {
        public static void NotifyPropertyChanged<T>(this IImplementsNotifyPropertyChanged host, Expression<Func<T>> lambda)
        {
            Contract.Requires(lambda != null);
            Contract.Requires(lambda.Body is MemberExpression);

            MemberExpression bodyExpression = (MemberExpression)lambda.Body;

            string propertyName = bodyExpression.Member.Name;

            if (!string.IsNullOrEmpty(propertyName))
            {
                host.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
