using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rashim.RND.ManagedDiscovery.DiscoverableService
{
     public class StringService : IStringService
    {
        public string ToUpper(string content)
        {
            return content.ToUpper();
        }
    }
}
