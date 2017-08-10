using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Northwind
{
	[DataContract]
	public class Territory
	{
		[DataMember]
		public string ID
		{
			get;
			set;
		}

		[DataMember]
		public string Description
		{
			get;
			set;
		}
	}
}
