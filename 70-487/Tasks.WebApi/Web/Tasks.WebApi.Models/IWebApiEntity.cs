using System;
using System.Collections.Generic;
namespace Tasks.WebApi.Models
{
    public interface IWebApiEntity
    {
        List<Link> Links
        {
            get;
        }
    }
}
