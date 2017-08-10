using System.Collections.Generic;

namespace Tasks.BusinessEntities
{
    public class Priority
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
    }
}
