using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

class SimpleSerial
{
	private static readonly SerialInfo[] _fmts = 
		{
			new SerialInfo(typeof(BinaryFormatter), "Employee.bin"),
			new SerialInfo(typeof(SoapFormatter), "Employee.soap")
		};
	private static void Main()
	{
		RunTest(TestVersionedDeserialization);
		RunTest(TestSerialization);
	}

	private static void RunTest(SerialInfoAction del)
	{
	 	if (null == del)
		{
			throw new ArgumentNullException("del");
		}

		foreach (SerialInfo fmt in _fmts)
		{
			del(fmt);
		}
	}

	private static void TestVersionedDeserialization(SerialInfo fmt)
	{
		string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		Employee e = Deserialize(fmt, Path.Combine(path, "oldVer"));
		Console.WriteLine("Old-version employee restored by {0}:\n{1}", 
			fmt.Formatter.GetType(),
			e);
	}

	private static void TestSerialization(SerialInfo fmt)
	{
		Employee e = new Employee("000-111-222", "Jeffrey Richter", 1972);
		Console.WriteLine("Original Employee:\n" + e.ToString());
		Serialize(fmt, e);

		Employee restored = Deserialize(fmt);
		Console.WriteLine("Restored Employee:\n" + restored.ToString());

		Console.WriteLine("Serialization test for {0} is {1}", fmt.Formatter.GetType(),
			e.Equals(restored) ? "OK" : "FAILED");
	}

	private static void Serialize(SerialInfo fmt, Employee e)
	{
		using (Stream writeFs = fmt.GetSerializationStream(false, null))
		{
			fmt.Formatter.Serialize(writeFs, e);
		}
	}

	private static Employee Deserialize(SerialInfo fmt)
	{
		return Deserialize(fmt, null);
	}

	private static Employee Deserialize(SerialInfo fmt, string pathPrefix)
	{
		using (Stream readFs = fmt.GetSerializationStream(true, pathPrefix))
		{
			return (Employee) fmt.Formatter.Deserialize(readFs);
		}
	}
}