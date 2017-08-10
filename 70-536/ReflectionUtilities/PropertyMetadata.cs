using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace ReflectionUtilities
{
	public sealed class PropertyMetadata
	{
		private readonly PropertyInfo _propertyInfo;
		private readonly Lazy<DisplayAttribute> _displayAttribute;
		private readonly Lazy<DisplayFormatAttribute> _formatAttribute;

		private PropertyMetadata(PropertyInfo propertyInfo)
		{
			Contract.Requires(propertyInfo != null);

			_propertyInfo = propertyInfo;
			_displayAttribute = new Lazy<DisplayAttribute>(FindAttribute<DisplayAttribute>);
			_formatAttribute = new Lazy<DisplayFormatAttribute>(FindAttribute<DisplayFormatAttribute>);
		}

		public string Caption
		{
			get
			{
				if (_displayAttribute.Value != null)
				{
					return !String.IsNullOrEmpty(_displayAttribute.Value.ShortName) ?
						_displayAttribute.Value.ShortName :
						_displayAttribute.Value.Name;
				}

				return _propertyInfo.Name;
			}
		}

		public string GetValue(object instance)
		{
			Contract.Requires(instance != null);

			object rawValue = _propertyInfo.GetValue(instance, null);

			if (_formatAttribute.Value != null)
			{
				if (null == rawValue && !String.IsNullOrEmpty(_formatAttribute.Value.NullDisplayText))
				{
					return _formatAttribute.Value.NullDisplayText;
				}

				if (!String.IsNullOrEmpty(_formatAttribute.Value.DataFormatString))
				{
					return String.Format(_formatAttribute.Value.DataFormatString, rawValue);
				}
			}

			return Convert.ToString(rawValue);
		}

		public static IEnumerable<PropertyMetadata> ParseDataType(Type type)
		{
			Contract.Requires(type != null);

			Contract.Ensures(Contract.Result<IEnumerable<PropertyMetadata>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<PropertyMetadata>>(), m => m != null));

			return from pi in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
			       let pt = pi.PropertyType
			       where pt.IsPrimitive || typeof(string) == pt || typeof(DateTime) == pt
			       select new PropertyMetadata(pi);
		}

		private T FindAttribute<T>()
		{
			return _propertyInfo.GetCustomAttributes(typeof(T), true).Cast<T>().SingleOrDefault();
		}
	}
}