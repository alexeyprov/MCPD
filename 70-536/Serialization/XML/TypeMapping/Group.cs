using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TypeMapping
{
	public class Group
	{
		[SoapAttribute(Namespace = "http://www.cpandl.com")]
		public string GroupName;

		[SoapAttribute(DataType = "base64Binary")]
		public Byte[] GroupNumber;

		[SoapAttribute(DataType = "date", AttributeName = "CreationDate")]
		public DateTime Today;

		[SoapElement(DataType = "nonNegativeInteger", ElementName = "PosInt")]
		public string PostitiveInt;

		// This is ignored when serialized unless it's overridden.
		[SoapIgnore]
		public bool IgnoreThis;

		public GroupType Grouptype;

		public Vehicle MyVehicle;

		// The SoapInclude allows the method to return a Car.
		[SoapInclude(typeof(Car))]
		public Vehicle MyCar(string licNumber)
		{
			Vehicle v = new Car();
			v.licenseNumber = ("" == licNumber) ? "!!!!!!" : licNumber;
			return v;
		}
	}

	public enum GroupType
	{
		// These enums can be overridden.
		[SoapEnum("Small")]
		A,
		[SoapEnum("Large")]
		B
	}
}
