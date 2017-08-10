using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionUtilities
{
	public class CompositePropertyAccessor
	{
		#region Data Members

		private object _currentTarget;
		private PropertyDescriptorCollection _rootProperties;
		private Dictionary<string, List<PropertyDescriptor>> _descriptorsMap;
		private List<PropertyDescriptor> _currentDescriptors;

		#endregion

		#region Construction

		public CompositePropertyAccessor(Type rootType)
		{
			_rootProperties = TypeDescriptor.GetProperties(rootType);
			_descriptorsMap = new Dictionary<string, List<PropertyDescriptor>>();
		}

		#endregion

		#region Public Methods

		public PropertyDescriptor GetDescriptor(ref object target, string propertyName)
		{
			InitializeRecursiveSearch(target, propertyName);
			PropertyDescriptor descriptor = GetDescriptorHelper(_rootProperties, propertyName);
			target = _currentTarget;
			return descriptor;
		}

		public object GetValue(object target, string propertyName)
		{
			List<PropertyDescriptor> descriptors;
			object retval = null;

			if (_descriptorsMap.TryGetValue(propertyName, out descriptors))
			{
				retval = target;
				foreach (PropertyDescriptor descriptor in descriptors)
				{
					retval = descriptor.GetValue(retval);
				}
			}
			else
			{
				InitializeRecursiveSearch(target, propertyName);
				retval = GetDescriptorHelper(_rootProperties, propertyName).GetValue(_currentTarget);
			}

			return retval;
		}

		#endregion

		#region Implementation

		private PropertyDescriptor GetDescriptorHelper(PropertyDescriptorCollection properties, string propertyName)
		{
			int dotIndex = propertyName.IndexOf('.');
			PropertyDescriptor descriptor = null;

			if (dotIndex > 0)
			{
				// set up a new target
				string subPropertyName = propertyName.Substring(0, dotIndex);
				PropertyDescriptor subDescriptor = properties.Find(subPropertyName, true);

				if (subDescriptor != null)
				{
					_currentDescriptors.Add(subDescriptor);

					if (null == _currentTarget)
					{
						// get properties collection based on type
						TypeDescriptor.GetProperties(subDescriptor.PropertyType);
					}
					else
					{
						// get properties collection based on value and change target
						_currentTarget = subDescriptor.GetValue(_currentTarget);
						properties = TypeDescriptor.GetProperties(_currentTarget);
					}

					// perform a recursive search on the new target
					descriptor = GetDescriptorHelper(properties, propertyName.Substring(dotIndex + 1));
				}
			}
			else
			{
				descriptor = properties.Find(propertyName, true);
				if (descriptor != null)
				{
					_currentDescriptors.Add(descriptor);
				}
			}

			return descriptor;
		}

		private void InitializeRecursiveSearch(object target, string propertyName)
		{
			_currentTarget = target;

			if (_descriptorsMap.TryGetValue(propertyName, out _currentDescriptors))
			{
				_currentDescriptors.Clear();
			}
			else
			{
				_currentDescriptors = new List<PropertyDescriptor>();
				_descriptorsMap[propertyName] = _currentDescriptors;
			}
		}

		#endregion

	}
}
