using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    internal sealed class PriorityMapper : ITypeMapper<Data.Models.Priority, Priority>
    {
        #region ITypeMapper<Priority,Priority> Members

        Priority ITypeMapper<Data.Models.Priority, Priority>.Create(Data.Models.Priority from)
        {
            return new Priority
            {
                PriorityId = from.PriorityId,
                Name = from.Name,
                Ordinal = from.Ordinal,
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Title = "self",
                        Href = "/api/priorities/" + from.PriorityId,
                    },
                    new Link
                    {
                        Rel = "all",
                        Title = "All Priorities",
                        Href = "/api/priorities"
                    }
                }
            };
        }

        #endregion
    }
}