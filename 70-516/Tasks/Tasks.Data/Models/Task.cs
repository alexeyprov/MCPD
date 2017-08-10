using System;
using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.Data.Models
{
    public class Task : IVersionedModel, IKeyedEntity<long>
    {
        private readonly IList<User> _assignees;
        private readonly IList<Category> _categories;

        public Task()
        {
            _assignees = new List<User>();
            _categories = new List<Category>();
        }

        public virtual long TaskId
        {
            get;
            set;
        }

        public virtual string Subject
        {
            get;
            set;
        }

        public virtual DateTime? StartDate
        {
            get;
            set;
        }

        public virtual DateTime? DueDate
        {
            get;
            set;
        }

        public virtual DateTime? DateCompleted
        {
            get;
            set;
        }

        public virtual IList<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public virtual Priority Priority
        {
            get;
            set;
        }

        public virtual Status Status
        {
            get;
            set;
        }

        public virtual byte[] Version
        {
            get;
            set;
        }

        public virtual DateTime CreatedDate
        {
            get;
            set;
        }

        public virtual User CreatedBy
        {
            get;
            set;
        }

        public virtual IList<User> Assignees
        {
            get
            {
                return _assignees;
            }
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
