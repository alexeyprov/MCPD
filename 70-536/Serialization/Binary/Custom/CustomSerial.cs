using System;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

class SimpleSerial
{
	private const double VAT = 1.18;

	static void Main()
	{
		TypeTest(new OrderItem("Cool gadget", 28.75m, 4, VAT));
		TypeTest(new Employee("Jeffrey Richter", 1972));
	}

	static void TypeTest<T>(T t) where T : IEquatable<T>
	{
		ListDictionary ld = new ListDictionary();
		ld[OrderItem.VALUE_ADDED_TAX_FIELD] = VAT;

		StreamingContext ctx = new StreamingContext(StreamingContextStates.File, ld);

		string fileStem = typeof(T).ToString().Trim();
		TestSerialization(new BinaryFormatter(null, ctx), fileStem + ".bin", t);
		TestSerialization(new SoapFormatter(null, ctx), fileStem + ".soap", t);
	}

	static void TestSerialization<T>(IFormatter formatter, string fileName, T t) where T : IEquatable<T>
	{
		using (FileStream writeFs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
		{
			formatter.Serialize(writeFs, t);
		}
		T restored;
		using (FileStream readFs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
		{
			restored = (T) formatter.Deserialize(readFs);
		}
		Console.WriteLine("Serialization test for {0} is {1}", formatter.GetType(),
			t.Equals(restored) ? "OK" : "FAILED");
		Console.WriteLine("Before:{0}{2}After:{1}{2}{2}",
			t,
			restored,
			Environment.NewLine);
	}
}