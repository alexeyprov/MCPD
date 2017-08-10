using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.WebApi.Models
{
    public class Priority : IWebApiEntity, IKeyedEntity<long>
    {
        public long PriorityId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int Ordinal
        {
            get;
            set;
        }
        public List<Link> Links
        {
            get;
            set;
        }

        #region IKeyedEntity<long> Members

        long IKeyedEntity<long>.Id
        {
            get
            {
                return PriorityId;
            }
        }

        #endregion
    }
}
