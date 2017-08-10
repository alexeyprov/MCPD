using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal sealed class StatusMap : VersionedClassMap<Status>
    {
        public StatusMap()
        {
            Id(m => m.StatusId);

            Map(m => m.Name).Not.Nullable();
            Map(m => m.Ordinal).Not.Nullable();
        }
    }
}
