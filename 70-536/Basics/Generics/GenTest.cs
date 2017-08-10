using System;
using System.Collections.Generic;

using TemperatureScale = System.Collections.Generic.SortedDictionary<Temperature, string>;
using TempScaleEntry = System.Collections.Generic.KeyValuePair<Temperature, string>;

sealed class GenTest
{
	static void Main()
	{
		TemperatureScale scale = new TemperatureScale();
		scale.Add(new Temperature(2017.15), "Boiling point of Lead");
		scale.Add(new Temperature(0), "Absolute zero");
	        scale.Add(new Temperature(273.15), "Freezing point of water");
        	scale.Add(new Temperature(5100.15), "Boiling point of Carbon");
	        scale.Add(new Temperature(373.15), "Boiling point of water");
	        scale.Add(new Temperature(600.65), "Melting point of Lead");


		foreach (TempScaleEntry tse in scale)
		{
			Console.WriteLine("{0} is {1}", tse.Value, tse.Key.Celsius);
		}
	}
}