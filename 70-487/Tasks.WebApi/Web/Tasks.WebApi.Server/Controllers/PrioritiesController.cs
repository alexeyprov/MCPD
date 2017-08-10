using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class PrioritiesController : ApiController
    {
        private readonly ISession _session;
        private readonly IHttpFetcher<long, Data.Models.Priority> _fetcher;
        private readonly ITypeMapper<Data.Models.Priority, Priority> _mapper;

        public PrioritiesController(
            ISession session,
            IHttpFetcher<long, Data.Models.Priority> fetcher,
            ITypeMapper<Data.Models.Priority, Priority> mapper)
        {
            _session = session;
            _fetcher = fetcher;
            _mapper = mapper;
        }

        // GET: api/Priorities
        public IEnumerable<Priority> Get()
        {
            return _session
                .QueryOver<Data.Models.Priority>()
                .List()
                .Select(_mapper.Create);
        }

        // GET: api/Priorities/5
        public Priority Get(long id)
        {
            Data.Models.Priority p = _fetcher.GetModel(id);
            return _mapper.Create(p);
        }
    }
}
