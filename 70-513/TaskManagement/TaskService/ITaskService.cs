using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using TaskService.BusinessEntities;

namespace TaskService
{
	[ServiceContract]
	public interface ITaskService
	{
		[OperationContract]
		int AddTask(Task t);

		[OperationContract]
		[FaultContract(typeof(FaultInfo))]
		Task GetTask(int taskNumber);

		[OperationContract]
		[FaultContract(typeof(FaultInfo))]
		void AssignTask(int taskNumber, string owner);

		[OperationContract]
		Task[] GetTasksByOwner(string owner);

		[OperationContract]
		[FaultContract(typeof(FaultInfo))]
		bool IsTaskCompleted(int taskNumber);

		[OperationContract(IsOneWay = true)]
		void MarkTaskCompleted(int taskNumber);

		[OperationContract(IsOneWay = true)]
		void DeleteTask(int taskNumber);
	}
}
