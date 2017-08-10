using System;
using System.ServiceModel;
using TaskService.BusinessEntities;

namespace TaskService
{
    [ServiceContract(Name = "TaskService", Namespace = "http://alexeypr.com/2015/05/Tasks")]
    public interface ITaskService
    {
        /// <summary>
        /// New async pattern
        /// </summary>
        [OperationContract]
        System.Threading.Tasks.Task<int> AddTask(Task t);

        [OperationContract]
        [FaultContract(typeof(FaultInfo))]
        Task GetTask(int taskNumber);

        /// <summary>
        /// Old async pattern
        /// </summary>
        [OperationContract(AsyncPattern=true)]
        [FaultContract(typeof(FaultInfo))]
        IAsyncResult BeginAssignTask(int taskNumber, string owner, AsyncCallback cb, object userState);

        int EndAssignTask(out string previousOwner, IAsyncResult asyncResult);

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
