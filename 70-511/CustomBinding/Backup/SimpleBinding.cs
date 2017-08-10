using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CustomBinding
{
	class SimpleBinding
	{
		#region Fields

		object _dataSource;
		PropertyDescriptor _dsPropDescr;
		Component _component;
		PropertyDescriptor _cPropDescr;
		bool _busy;

		#endregion

		#region Construction

		public SimpleBinding(object ds, string dsProperty, Component c, string cProperty)
		{
			if (null == ds)
			{
				throw new ArgumentNullException("ds");
			}
			if (String.IsNullOrEmpty(dsProperty))
			{
				throw new ArgumentNullException("dsProperty");
			}

			_dsPropDescr = TypeDescriptor.GetProperties(ds)[dsProperty];
			if (null == _dsPropDescr)
			{
				throw new ArgumentException("dsProperty");
			}
			_dsPropDescr.AddValueChanged(ds, new EventHandler(DataSource_ValueChanged));

			if (null == c)
			{
				throw new ArgumentNullException("c");
			}
			if (String.IsNullOrEmpty(cProperty))
			{
				throw new ArgumentNullException("cProperty");
			}

			_cPropDescr = TypeDescriptor.GetProperties(c)[cProperty];
			if (null == _cPropDescr)
			{
				throw new ArgumentException("cProperty");
			}
			_cPropDescr.AddValueChanged(c, new EventHandler(Component_ValueChanged));

			_dataSource = ds;
			_component = c;
		}

		#endregion

		#region Event Handlers

		void DataSource_ValueChanged(object sender, EventArgs e)
		{
			if (_busy)
			{
				return;
			}

			try
			{
				_busy = true;
				object value = _dsPropDescr.GetValue(_dataSource);
				if (!_cPropDescr.PropertyType.Equals(_dsPropDescr.PropertyType))
				{
					if (_cPropDescr.Converter.CanConvertFrom(_dsPropDescr.PropertyType))
					{
						value = _cPropDescr.Converter.ConvertFrom(value);
					}
					else if (_dsPropDescr.Converter.CanConvertTo(_cPropDescr.PropertyType))
					{
						value = _dsPropDescr.Converter.ConvertTo(value, _cPropDescr.PropertyType);
					}
				}
				_cPropDescr.SetValue(_component, value);
			}
			finally
			{
				_busy = false;
			}
		}

		void Component_ValueChanged(object sender, EventArgs e)
		{
			if (_busy)
			{
				return;
			}

			try
			{
				_busy = true;
				object value = _cPropDescr.GetValue(_component);
				if (!_cPropDescr.PropertyType.Equals(_dsPropDescr.PropertyType))
				{
					if (_dsPropDescr.Converter.CanConvertFrom(_cPropDescr.PropertyType))
					{
						value = _dsPropDescr.Converter.ConvertFrom(value);
					}
					else if (_cPropDescr.Converter.CanConvertTo(_dsPropDescr.PropertyType))
					{
						value = _cPropDescr.Converter.ConvertTo(value, _dsPropDescr.PropertyType);
					}
				}
				_dsPropDescr.SetValue(_dataSource, value);
			}
			finally
			{
				_busy = false;
			}
		}

		#endregion
	}
}
