using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace CasinoModel
{
    public class SomeOtherContext:DbContext
    {
 
        public SomeOtherContext(DbConnection connection)
            :base(connection,contextOwnsConnection:false )
        {
                
        }

        public SomeOtherContext()
        {
            
        }

        public DbSet<Casino> Casinos { get; set; }
    }
}
