using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cuboid.Shared
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _propertyChanged;

        #region INotifyPropertyChanged Members

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        #endregion

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> lambda)
        {
            Contract.Requires(lambda != null);
            Contract.Requires(lambda.Body is MemberExpression);

            MemberExpression bodyExpression = (MemberExpression)lambda.Body;

            string propertyName = bodyExpression.Member.Name;

            if (!string.IsNullOrEmpty(propertyName) && _propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
