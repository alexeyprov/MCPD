using System;
using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.WebApi.Models
{
    public class User : IWebApiEntity, IKeyedEntity<Guid>
    {
        public Guid UserId
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public List<Link> Links
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
