using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    internal sealed class StatusMapper : ITypeMapper<Data.Models.Status, Status>
    {
        #region ITypeMapper<Status,Status> Members

        Status ITypeMapper<Data.Models.Status, Status>.Create(Data.Models.Status from)
        {
            return new Status
            {
                StatusId = from.StatusId,
                Name = from.Name,
                Ordinal = from.Ordinal,
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Title = "self",
                        Href = "/api/statuses/" + from.StatusId
                    },
                    new Link
                    {
                        Rel = "all",
                        Title = "All Statuses",
                        Href = "/api/statuses"
                    }
                }
            };
        }

        #endregion
    }
}