using System;
using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.Data.Models
{
    public class User : IVersionedModel, IKeyedEntity<Guid>
    {
        public virtual Guid UserId
        {
            get;
            set;
        }
        public virtual string UserName
        {
            get;
            set;
        }
        public virtual string FirstName
        {
            get;
            set;
        }
        public virtual string LastName
        {
            get;
            set;
        }
        public virtual string Email
        {
            get;
            set;
        }
        public virtual byte[] Version
        {
            get;
            set;
        }

        #region IKeyedEntity<Guid> Members

        Guid IKeyedEntity<Guid>.Id
        {
            get
            {
                return UserId;
            }
        }

        #endregion
    }
}
