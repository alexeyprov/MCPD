using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal sealed class PriorityMap : VersionedClassMap<Priority>
    {
        public PriorityMap()
        {
            Id(m => m.PriorityId);

            Map(m => m.Name).Not.Nullable();
            Map(m => m.Ordinal).Not.Nullable();
        }
    }
}
