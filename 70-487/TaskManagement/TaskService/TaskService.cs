using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using TaskService.BusinessEntities;

namespace TaskService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TaskService : ITaskService
    {
        #region Private Fields

        private static readonly IDictionary<int, Task> _tasks;
        private static int _lastTaskId;

        #endregion

        #region Constructor

        static TaskService()
        {
            _tasks = new Dictionary<int, Task>();
        }

        #endregion

        #region ITaskService Members

        System.Threading.Tasks.Task<int> ITaskService.AddTask(Task t)
        {
            return System.Threading.Tasks.Task.Run(
                () =>
                {
                    _tasks.Add(++_lastTaskId, t);
                    t.TaskNumber = _lastTaskId;
                    return _lastTaskId;
                });
        }

        IAsyncResult ITaskService.BeginAssignTask(int taskNumber, string owner, AsyncCallback cb, object userState)
        {
            System.Threading.Tasks.Task<object> task = System.Threading.Tasks.Task.Run(
                () => (object)new
                {
                    Task = GetTaskSafe(taskNumber),
                    Owner = owner
                });
            task.ContinueWith(t => cb(t));

            return task;
        }

        int ITaskService.EndAssignTask(out string previousOwner, IAsyncResult asyncResult)
        {
            dynamic payload;
            try
            {
                payload = ((System.Threading.Tasks.Task<object>)asyncResult).Result;
            }
            catch (AggregateException aex)
            {
                Exception bex = aex.GetBaseException();
                if (bex is FaultException<FaultInfo>)
                {
                    throw bex;
                }

                throw;
            }
            
            Task task = payload.Task;
            previousOwner = task.AssignedTo;
            task.AssignedTo = payload.Owner;

            return task.TaskNumber;
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
            Task task = GetTaskSafe(taskNumber);

            task.Status = (task.DueDate < DateTime.Now) ?
                TaskStatus.CompletedLate :
                TaskStatus.CompletedOnTime;
        }

        void ITaskService.DeleteTask(int taskNumber)
        {
            GetTaskSafe(taskNumber);

            _tasks.Remove(taskNumber);
        }

        #endregion

        #region Implementation

        private Task GetTaskSafe(int taskNumber)
        {
            Task task;

            if (taskNumber < 0 || !_tasks.TryGetValue(taskNumber, out task))
            {
                string message = string.Format("Invalid task number: {0}", taskNumber);

                Console.WriteLine(message);

                throw new FaultException<FaultInfo>(
                    new FaultInfo
                    {
                        ErrorMessage = message
                    });
            }

            return task;
        } 

        #endregion
    }
}
