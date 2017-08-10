using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TaskService.BusinessEntities
{
	[DataContract]
	public class FaultInfo
	{
		[DataMember]
		public string ErrorMessage
		{
			get;
			set;
		}
	}
}
