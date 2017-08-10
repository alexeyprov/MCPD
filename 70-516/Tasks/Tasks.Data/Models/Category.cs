using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.Data.Models
{
    public class Category : IVersionedModel, IKeyedEntity<long>
    {
        public virtual long CategoryId
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }
        public virtual string Description
        {
            get;
            set;
        }
        public virtual byte[] Version
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
