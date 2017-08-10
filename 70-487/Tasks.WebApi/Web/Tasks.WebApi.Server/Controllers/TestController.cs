using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Tasks.WebApi.Server.Models;

namespace Tasks.WebApi.Server.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public IEnumerable<string> Get(
            [FromUri] SearchParameters searchParameters,
            [FromUri] SearchParametersEx searchExParameters)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
