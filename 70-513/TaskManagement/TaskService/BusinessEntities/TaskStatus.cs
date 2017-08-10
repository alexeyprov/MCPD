using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TaskService.BusinessEntities
{
	[DataContract]
	public enum TaskStatus
	{
		[EnumMember]
		New,

		[EnumMember]
		Assigned,

		[EnumMember]
		Overdue,

		[EnumMember]
		CompletedOnTime,

		[EnumMember]
		CompletedLate
	}
}
