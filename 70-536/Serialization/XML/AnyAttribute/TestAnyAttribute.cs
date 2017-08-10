using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

class TestAnyAttribute
{
	private const string INPUT_FILE = "Group.xml";
	private const string OUTPUT_FILE = "GroupWithAttrs.xml";


	private TestAnyAttribute()
	{
		_ser = new XmlSerializer(typeof(Group));
	}

	public static void Main()
	{
		TestAnyAttribute test = new TestAnyAttribute();
		Group g = (Group) test.Deserialize(INPUT_FILE);

		Console.WriteLine("Unknown attributes found:");
		foreach (XmlAttribute xa in g.UnkAttributes)
		{
			Console.WriteLine("{0} = {1}", xa.Name, xa.InnerXml);
		}

		test.Serialize(OUTPUT_FILE, g);		
	}

	private void Serialize(string fileName, object o)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Create))
		{
			_ser.Serialize(fs, o);
		}
	}

	private object Deserialize(string fileName)
	{
		using (FileStream fs = new FileStream(fileName, FileMode.Open))
		{
			return _ser.Deserialize(fs);
		}
	}

	private XmlSerializer _ser;
	
}