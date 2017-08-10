using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.WebApi.Models
{
    public class Category : IWebApiEntity, IKeyedEntity<long>
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

        #region IKeyedEntity<long> Members

        long IKeyedEntity<long>.Id
        {
            get
            {
                return CategoryId;
            }
        }

        #endregion
    }
}
