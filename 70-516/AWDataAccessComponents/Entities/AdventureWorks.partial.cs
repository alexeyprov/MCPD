using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;

namespace AdventureWorks.Data.Entities
{
    public partial class Entities : DbContext
    {
        public Entities(EntityConnection cn)
            : base(cn, false)
        {
        }
    }
}