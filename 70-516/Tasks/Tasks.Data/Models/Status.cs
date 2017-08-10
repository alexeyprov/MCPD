using System.Collections.Generic;
using Tasks.Common.Interface;

namespace Tasks.Data.Models
{
    public class Status : IVersionedModel, IKeyedEntity<long>
    {
        public virtual long StatusId
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }
        public virtual int Ordinal
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
                return StatusId;
            }
        }

        #endregion
    }
}
