using System;
using System.IO;
using System.Xml.Serialization;

class TestChoice
{
	private XmlSerializer _ser = new XmlSerializer(typeof(Car));
	private const string FILE_NAME = "Car.xml";

	static void Main()
	{
		Car original = Car.CreateInstance();
		TestChoice app = new TestChoice();
		Console.WriteLine("Original car:{1}{0}{1}",
			original,
			Environment.NewLine);
		app.Serialize(original, FILE_NAME);

		Car restored = app.Deserialize(FILE_NAME);
		Console.WriteLine("Restored car:{1}{0}",
			restored,
			Environment.NewLine);
	}

	private void Serialize(Car c, string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Create))
		{
			_ser.Serialize(fs, c);
		}
	}

	private Car Deserialize(string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Open))
		{
			return (Car) _ser.Deserialize(fs);
		}
	}
}