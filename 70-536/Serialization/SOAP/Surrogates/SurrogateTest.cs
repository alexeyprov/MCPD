using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

class SurrogateTest
{
	private static void Main()
	{
		const string FILE_NAME = "Car.soap";
		IFormatter fmt = new SoapFormatter();
		StreamingContext ctx = new StreamingContext(StreamingContextStates.All);
		SurrogateSelector ss = new SurrogateSelector();

		ss.AddSurrogate(typeof(Car), ctx, new CarSurrogate());
		fmt.SurrogateSelector = ss;

		object original = GetObject();
		Console.WriteLine("Before: {0}", original);

		Serialize(fmt, original, FILE_NAME);
		object restored = Deserialize(fmt, FILE_NAME);
   		Console.WriteLine("After: {0}", restored);

		ss.RemoveSurrogate(typeof(Car), ctx);
	}

	private static void Serialize(IFormatter fmt, object o, string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Create))
		{
			fmt.Serialize(fs, o);
		}
	}

	private static object Deserialize(IFormatter fmt, string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Open))
		{
			return fmt.Deserialize(fs);
		}
	}

	private static object GetObject()
	{
		return new Car("Nissan", "Almera Classic");
	}
}