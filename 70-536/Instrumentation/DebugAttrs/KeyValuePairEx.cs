using System;
using System.Diagnostics;

namespace DebugAttrs
{
	[DebuggerDisplay("{_value}", Name = "{_key}")]
	class KeyValuePairEx
	{
		object _key;
		object _value;

		public KeyValuePairEx(object key, object value)
		{
			_key = key;
			_value = value;
		}
	}
}
