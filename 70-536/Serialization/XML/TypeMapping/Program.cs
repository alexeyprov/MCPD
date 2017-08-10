using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace TypeMapping
{
	sealed class Program
	{
		public static void Main()
		{
			Program test = new Program();

			XmlSerializer origSer = CreateOriginalSerializer();
			XmlSerializer overSer = CreateOverrideSerializer();

			test.Serialize("SoapOriginal.xml", origSer);
			test.Serialize("SoapOverrides.xml", overSer);

			// Simple XmlSerializer do not handle SoapXxx attributes
			// It understands XmlXxx attributes only.
			//test.Deserialize("SoapOriginal.xml", new XmlSerializer(typeof(Group)));
			test.Deserialize("SoapOriginal.xml", origSer);
			test.Deserialize("SoapOverrides.xml", overSer);
		}

		Program()
		{
			_group = MakeGroup();
		}

#region Serializers
		private static XmlSerializer CreateOriginalSerializer()
		{
			// Create an instance of the XmlSerializer class.
			XmlTypeMapping myMapping =
			(new SoapReflectionImporter().ImportTypeMapping(typeof(Group)));

			return new XmlSerializer(myMapping);
		}

		private static XmlSerializer CreateOverrideSerializer()
		{
			SoapAttributeOverrides mySoapAttributeOverrides =
			new SoapAttributeOverrides();
			SoapAttributes soapAtts = new SoapAttributes();

			soapAtts.SoapElement = new SoapElementAttribute("xxxx");
			mySoapAttributeOverrides.Add(typeof(Group), "PostitiveInt",	soapAtts);

			// Override the IgnoreThis property.
			soapAtts = new SoapAttributes();
			soapAtts.SoapIgnore = false;
			mySoapAttributeOverrides.Add(typeof(Group), "IgnoreThis", soapAtts);

			// Override the GroupType enumeration.    
			soapAtts = new SoapAttributes();
			soapAtts.SoapEnum = new SoapEnumAttribute("ZeroTo1000");

			// Add the SoapAttributes to the 
			// mySoapAttributeOverridesrides object.
			mySoapAttributeOverrides.Add(typeof(GroupType), "A", soapAtts);

			// Create second enumeration and add it.
			soapAtts = new SoapAttributes();
			soapAtts.SoapEnum = new SoapEnumAttribute("Over1000");
			mySoapAttributeOverrides.Add(typeof(GroupType), "B", soapAtts);

			// Override the Group type.
			soapAtts = new SoapAttributes();
			SoapTypeAttribute soapType = new SoapTypeAttribute();
			soapType.TypeName = "Team";
			soapAtts.SoapType = soapType;
			mySoapAttributeOverrides.Add(typeof(Group), soapAtts);

			// Create an XmlTypeMapping that is used to create an instance 
			// of the XmlSerializer. Then return the XmlSerializer object.
			XmlTypeMapping myMapping = (new SoapReflectionImporter(
			mySoapAttributeOverrides)).ImportTypeMapping(typeof(Group));

			XmlSerializer ser = new XmlSerializer(myMapping);
			return ser;
		}
#endregion

#region Helpers
		private static Group MakeGroup()
		{
			// Create an instance of the class that will be serialized.
			Group myGroup = new Group();

			// Set the object properties.
			myGroup.GroupName = ".NET";

			myGroup.GroupNumber = new Byte[2] {
				Convert.ToByte(100), Convert.ToByte(50)};

			myGroup.Today = new DateTime(1978, 12, 7);

			myGroup.PostitiveInt = "10000";
			myGroup.IgnoreThis = true;
			myGroup.Grouptype = GroupType.B;
			Car thisCar = (Car) myGroup.MyCar("1234566");
			myGroup.MyVehicle = thisCar;
			return myGroup;
		}
#endregion

#region I/O
		private static void DumpGroup(Group myGroup)
		{
			Console.WriteLine(myGroup.GroupName);
			Console.WriteLine(myGroup.GroupNumber[0]);
			Console.WriteLine(myGroup.GroupNumber[1]);
			Console.WriteLine(myGroup.Today);
			Console.WriteLine(myGroup.PostitiveInt);
			Console.WriteLine(myGroup.IgnoreThis);
			Console.WriteLine();
		}

		private void Serialize(string filename, XmlSerializer serializer)
		{
			// Writing the file requires a TextWriter.
			using (XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
			{
				writer.Formatting = Formatting.Indented;
				writer.WriteStartElement("wrapper");
				// Serialize the class, and close the TextWriter.
				serializer.Serialize(writer, _group);
				writer.WriteEndElement();
			}
		}

		private void Deserialize(string filename, XmlSerializer serializer)
		{
			// Reading the file requires an  XmlTextReader.
			using (XmlTextReader reader = new XmlTextReader(filename))
			{
				reader.ReadStartElement("wrapper");

				// Deserialize and cast the object.
				Group myGroup = (Group) serializer.Deserialize(reader);
				DumpGroup(myGroup);
				reader.ReadEndElement();
			}
		}
#endregion

		private Group _group;
	}
}
