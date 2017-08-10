using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.WebApi.Common.Interface
{
    public class MembershipUserWrapper
    {
        public Guid UserId
        {
            get;
            set;
        }
        public string Username
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
    }
}
