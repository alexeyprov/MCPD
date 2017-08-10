using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.WebApi.Server.Models
{
    public class SearchParameters
    {
        public string Name
        {
            get;
            set;
        }

        public DateTime DateOfBirth
        {
            get;
            set;
        }
    }
}