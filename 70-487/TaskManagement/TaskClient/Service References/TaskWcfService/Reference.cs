﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskClient.TaskWcfService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
    [System.SerializableAttribute()]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AssignedToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DueDateField;
        
        private int TaskNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TaskClient.TaskWcfService.TaskStatus StatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AssignedTo {
            get {
                return this.AssignedToField;
            }
            set {
                if ((object.ReferenceEquals(this.AssignedToField, value) != true)) {
                    this.AssignedToField = value;
                    this.RaisePropertyChanged("AssignedTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DueDate {
            get {
                return this.DueDateField;
            }
            set {
                if ((this.DueDateField.Equals(value) != true)) {
                    this.DueDateField = value;
                    this.RaisePropertyChanged("DueDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int TaskNumber {
            get {
                return this.TaskNumberField;
            }
            set {
                if ((this.TaskNumberField.Equals(value) != true)) {
                    this.TaskNumberField = value;
                    this.RaisePropertyChanged("TaskNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public TaskClient.TaskWcfService.TaskStatus Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskStatus", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
    public enum TaskStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        New = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Assigned = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Overdue = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CompletedOnTime = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CompletedLate = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FaultInfo", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
    [System.SerializableAttribute()]
    public partial class FaultInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://alexeypr.com/2015/05/Tasks", ConfigurationName="TaskWcfService.TaskService")]
    public interface TaskService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/AddTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/AddTaskResponse")]
        int AddTask(TaskClient.TaskWcfService.Task t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/AddTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/AddTaskResponse")]
        System.Threading.Tasks.Task<int> AddTaskAsync(TaskClient.TaskWcfService.Task t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/GetTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/GetTaskResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TaskClient.TaskWcfService.FaultInfo), Action="http://alexeypr.com/2015/05/Tasks/TaskService/GetTaskFaultInfoFault", Name="FaultInfo", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
        TaskClient.TaskWcfService.Task GetTask(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/GetTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/GetTaskResponse")]
        System.Threading.Tasks.Task<TaskClient.TaskWcfService.Task> GetTaskAsync(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/AssignTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/AssignTaskResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TaskClient.TaskWcfService.FaultInfo), Action="http://alexeypr.com/2015/05/Tasks/TaskService/AssignTaskFaultInfoFault", Name="FaultInfo", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
        TaskClient.TaskWcfService.AssignTaskResponse AssignTask(TaskClient.TaskWcfService.AssignTaskRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/AssignTask", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/AssignTaskResponse")]
        System.Threading.Tasks.Task<TaskClient.TaskWcfService.AssignTaskResponse> AssignTaskAsync(TaskClient.TaskWcfService.AssignTaskRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/GetTasksByOwner", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/GetTasksByOwnerResponse")]
        TaskClient.TaskWcfService.Task[] GetTasksByOwner(string owner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/GetTasksByOwner", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/GetTasksByOwnerResponse")]
        System.Threading.Tasks.Task<TaskClient.TaskWcfService.Task[]> GetTasksByOwnerAsync(string owner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/IsTaskCompleted", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/IsTaskCompletedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TaskClient.TaskWcfService.FaultInfo), Action="http://alexeypr.com/2015/05/Tasks/TaskService/IsTaskCompletedFaultInfoFault", Name="FaultInfo", Namespace="http://schemas.datacontract.org/2004/07/TaskService.BusinessEntities")]
        bool IsTaskCompleted(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://alexeypr.com/2015/05/Tasks/TaskService/IsTaskCompleted", ReplyAction="http://alexeypr.com/2015/05/Tasks/TaskService/IsTaskCompletedResponse")]
        System.Threading.Tasks.Task<bool> IsTaskCompletedAsync(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/05/Tasks/TaskService/MarkTaskCompleted")]
        void MarkTaskCompleted(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/05/Tasks/TaskService/MarkTaskCompleted")]
        System.Threading.Tasks.Task MarkTaskCompletedAsync(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/05/Tasks/TaskService/DeleteTask")]
        void DeleteTask(int taskNumber);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://alexeypr.com/2015/05/Tasks/TaskService/DeleteTask")]
        System.Threading.Tasks.Task DeleteTaskAsync(int taskNumber);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AssignTask", WrapperNamespace="http://alexeypr.com/2015/05/Tasks", IsWrapped=true)]
    public partial class AssignTaskRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://alexeypr.com/2015/05/Tasks", Order=0)]
        public int taskNumber;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://alexeypr.com/2015/05/Tasks", Order=1)]
        public string owner;
        
        public AssignTaskRequest() {
        }
        
        public AssignTaskRequest(int taskNumber, string owner) {
            this.taskNumber = taskNumber;
            this.owner = owner;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AssignTaskResponse", WrapperNamespace="http://alexeypr.com/2015/05/Tasks", IsWrapped=true)]
    public partial class AssignTaskResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://alexeypr.com/2015/05/Tasks", Order=0)]
        public int AssignTaskResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://alexeypr.com/2015/05/Tasks", Order=1)]
        public string previousOwner;
        
        public AssignTaskResponse() {
        }
        
        public AssignTaskResponse(int AssignTaskResult, string previousOwner) {
            this.AssignTaskResult = AssignTaskResult;
            this.previousOwner = previousOwner;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TaskServiceChannel : TaskClient.TaskWcfService.TaskService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TaskServiceClient : System.ServiceModel.ClientBase<TaskClient.TaskWcfService.TaskService>, TaskClient.TaskWcfService.TaskService {
        
        public TaskServiceClient() {
        }
        
        public TaskServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TaskServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaskServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddTask(TaskClient.TaskWcfService.Task t) {
            return base.Channel.AddTask(t);
        }
        
        public System.Threading.Tasks.Task<int> AddTaskAsync(TaskClient.TaskWcfService.Task t) {
            return base.Channel.AddTaskAsync(t);
        }
        
        public TaskClient.TaskWcfService.Task GetTask(int taskNumber) {
            return base.Channel.GetTask(taskNumber);
        }
        
        public System.Threading.Tasks.Task<TaskClient.TaskWcfService.Task> GetTaskAsync(int taskNumber) {
            return base.Channel.GetTaskAsync(taskNumber);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TaskClient.TaskWcfService.AssignTaskResponse TaskClient.TaskWcfService.TaskService.AssignTask(TaskClient.TaskWcfService.AssignTaskRequest request) {
            return base.Channel.AssignTask(request);
        }
        
        public int AssignTask(int taskNumber, string owner, out string previousOwner) {
            TaskClient.TaskWcfService.AssignTaskRequest inValue = new TaskClient.TaskWcfService.AssignTaskRequest();
            inValue.taskNumber = taskNumber;
            inValue.owner = owner;
            TaskClient.TaskWcfService.AssignTaskResponse retVal = ((TaskClient.TaskWcfService.TaskService)(this)).AssignTask(inValue);
            previousOwner = retVal.previousOwner;
            return retVal.AssignTaskResult;
        }
        
        public System.Threading.Tasks.Task<TaskClient.TaskWcfService.AssignTaskResponse> AssignTaskAsync(TaskClient.TaskWcfService.AssignTaskRequest request) {
            return base.Channel.AssignTaskAsync(request);
        }
        
        public TaskClient.TaskWcfService.Task[] GetTasksByOwner(string owner) {
            return base.Channel.GetTasksByOwner(owner);
        }
        
        public System.Threading.Tasks.Task<TaskClient.TaskWcfService.Task[]> GetTasksByOwnerAsync(string owner) {
            return base.Channel.GetTasksByOwnerAsync(owner);
        }
        
        public bool IsTaskCompleted(int taskNumber) {
            return base.Channel.IsTaskCompleted(taskNumber);
        }
        
        public System.Threading.Tasks.Task<bool> IsTaskCompletedAsync(int taskNumber) {
            return base.Channel.IsTaskCompletedAsync(taskNumber);
        }
        
        public void MarkTaskCompleted(int taskNumber) {
            base.Channel.MarkTaskCompleted(taskNumber);
        }
        
        public System.Threading.Tasks.Task MarkTaskCompletedAsync(int taskNumber) {
            return base.Channel.MarkTaskCompletedAsync(taskNumber);
        }
        
        public void DeleteTask(int taskNumber) {
            base.Channel.DeleteTask(taskNumber);
        }
        
        public System.Threading.Tasks.Task DeleteTaskAsync(int taskNumber) {
            return base.Channel.DeleteTaskAsync(taskNumber);
        }
    }
}