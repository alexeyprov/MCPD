using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ReflectionUtilities
{
	/// <summary>
	/// The class uses reflection to compare composite property value of two objects
	/// </summary>
	public class CompositePropertyComparer<T> : IComparer<T>
	{
		#region Member Variables

		private string _propertyName;
		private CompositePropertyAccessor _propertyAccessor;

		#endregion

		#region Construction

		public CompositePropertyComparer(string propertyName)
		{
			_propertyName = propertyName;
			_propertyAccessor = new CompositePropertyAccessor(typeof(T));
		}

		#endregion

		#region IComparer<T> Members

		public int Compare(T x, T y)
		{
			if (null == x)
			{
				return -1;
			}

			if (null == y)
			{
				return 1;
			}

			object xValue = _propertyAccessor.GetValue(x, _propertyName);
			object yValue = _propertyAccessor.GetValue(y, _propertyName);

			return Comparer.Default.Compare(xValue, yValue);
		}

		#endregion
	}
}
