using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal sealed class CategoryMap : VersionedClassMap<Category>
    {
        public CategoryMap()
        {
            Id(m => m.CategoryId);

            Map(m => m.Name).Not.Nullable();
            Map(m => m.Description).Nullable();
        }
    }
}
