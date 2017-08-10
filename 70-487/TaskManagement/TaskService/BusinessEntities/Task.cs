using System;
using System.Runtime.Serialization;

namespace TaskService.BusinessEntities
{
    [DataContract]
    public class Task : IExtensibleDataObject
    {
        [DataMember(Order=0, IsRequired=true)]
        public int TaskNumber
        {
            get;
            set;
        }

        [DataMember(Order=1)]
        public TaskStatus Status
        {
            get;
            set;
        }

        [DataMember(Order=1)]
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

        #region IExtensibleDataObject Members

        ExtensionDataObject IExtensibleDataObject.ExtensionData
        {
            get;
            set;
        }

        #endregion
    }
}
