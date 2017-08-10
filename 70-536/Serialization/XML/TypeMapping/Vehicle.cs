using System;
using System.Xml.Serialization;

namespace TypeMapping
{
	// SoapInclude allows Vehicle to accept Car type.
	[SoapInclude(typeof(Car))]
	public abstract class Vehicle
	{
		public string licenseNumber;
		public DateTime makeDate;
	}

}
