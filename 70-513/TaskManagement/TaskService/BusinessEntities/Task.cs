using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TaskService.BusinessEntities
{
	[DataContract]
	public class Task
	{
		[DataMember]
		public int TaskNumber
		{
			get;
			set;
		}

		[DataMember]
		public TaskStatus Status
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

		[DataMember]
		public string AssignedTo
		{
			get;
			set;
		}

		[DataMember]
		public DateTime DueDate
		{
			get;
			set;
		}
	}
}
