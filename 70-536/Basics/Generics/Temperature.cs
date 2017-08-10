using System;

sealed class Temperature : IComparable<Temperature>
{
	double _kelvin;
	public const double CELSIUS_KELVIN = 273.15;

	public Temperature(double k)
	{
		Kelvin = k;
	}

	public double Kelvin
	{
		get
		{
			return _kelvin;
		}
		set
		{
			if (value < 0.0)
			{
				throw new ArgumentException();
			}
			_kelvin = value;
		}
	}

	public double Celsius
	{
		get
		{
			return _kelvin - CELSIUS_KELVIN;
		}
	}

	public int CompareTo(Temperature other)
	{
		return _kelvin.CompareTo(other._kelvin);
	}
}