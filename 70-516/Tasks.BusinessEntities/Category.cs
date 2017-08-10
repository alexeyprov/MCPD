using System.Collections.Generic;

namespace Tasks.BusinessEntities
{
    public class Category
    {
        public long CategoryId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
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
