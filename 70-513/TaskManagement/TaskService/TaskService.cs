using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using TaskService.BusinessEntities;

namespace TaskService
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class TaskService : ITaskService
	{
		private static IDictionary<int, Task> _tasks;
		private static int _lastTaskId;

		static TaskService()
		{
			_tasks = new Dictionary<int, Task>();
			_lastTaskId = 0;
		}

		#region ITaskService Members

		int ITaskService.AddTask(BusinessEntities.Task t)
		{
			_tasks.Add(++_lastTaskId, t);
			t.TaskNumber = _lastTaskId;
			return _lastTaskId;
		}

		void ITaskService.AssignTask(int taskNumber, string owner)
		{
			GetTaskSafe(taskNumber).AssignedTo = owner;
		}

		Task ITaskService.GetTask(int taskNumber)
		{
			return GetTaskSafe(taskNumber);
		}

		Task[] ITaskService.GetTasksByOwner(string owner)
		{
			return _tasks.Values.Where(t => t.AssignedTo == owner).ToArray();
		}

		bool ITaskService.IsTaskCompleted(int taskNumber)
		{
			Task task = GetTaskSafe(taskNumber);

			return (TaskStatus.CompletedOnTime == task.Status) ||
				(TaskStatus.CompletedLate == task.Status);
		}

		void ITaskService.MarkTaskCompleted(int taskNumber)
		{
			try
			{
				Task task = GetTaskSafe(taskNumber);

				task.Status = (task.DueDate < DateTime.Now) ?
					TaskStatus.CompletedLate :
					TaskStatus.CompletedOnTime;
			}
			catch (FaultException<string>)
			{
			}
		}

		void ITaskService.DeleteTask(int taskNumber)
		{
			try
			{
				GetTaskSafe(taskNumber);

				_tasks.Remove(taskNumber);
			}
			catch (FaultException<string>)
			{
			}
		}

		#endregion

		private Task GetTaskSafe(int taskNumber)
		{
			Task task;

			if (taskNumber < 0 || !_tasks.TryGetValue(taskNumber, out task))
			{
				string message = String.Format("Invalid task number: {0}", taskNumber);

				Console.WriteLine(message);

				throw new FaultException<FaultInfo>(
					new FaultInfo()
					{
						ErrorMessage = message
					});
			}

			return task;
		}
	}
}
