using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal sealed class UserMap : VersionedClassMap<User>
    {
        public UserMap()
        {
            Table("AllUsers");

            Id(m => m.UserId).CustomType<Guid>();

            Map(m => m.UserName).Not.Nullable();
            Map(m => m.FirstName).Not.Nullable();
            Map(m => m.LastName).Not.Nullable();
            Map(m => m.Email);
        }
    }
}
