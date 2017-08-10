using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NonSerializableClass
/// </summary>
//[Serializable]
public class NonSerializableClass
{
	private int _number;
	private string _string;

	public NonSerializableClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	public int NumericProperty
	{
		get
		{
			return _number;
		}
		set
		{
			_number = value;
		}
	}
	public string TextProperty
	{
		get
		{
			return _string;
		}
		set
		{
			_string = value;
		}
	}
}
