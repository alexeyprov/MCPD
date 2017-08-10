using System;
using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.WebApi.Models
{
    public class Task : IWebApiEntity, IKeyedEntity<long>
    {
        public long TaskId
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public DateTime? StartDate
        {
            get;
            set;
        }
        public DateTime? DueDate
        {
            get;
            set;
        }
        public DateTime? DateCompleted
        {
            get;
            set;
        }
        public List<Category> Categories
        {
            get;
            set;
        }
        public Priority Priority
        {
            get;
            set;
        }
        public Status Status
        {
            get;
            set;
        }
        public List<Link> Links
        {
            get;
            set;
        }
        public List<User> Assignees
        {
            get;
            set;
        }

        #region IKeyedEntity<long> Members

        long IKeyedEntity<long>.Id
        {
            get
            {
                return TaskId;
            }
        }

        #endregion
    }
}
