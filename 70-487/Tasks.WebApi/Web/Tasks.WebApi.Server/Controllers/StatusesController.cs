using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class StatusesController : ApiController
    {
        private readonly ISession _session;
        private readonly IHttpFetcher<long, Data.Models.Status> _fetcher;
        private readonly ITypeMapper<Data.Models.Status, Status> _mapper;

        public StatusesController(
            ISession session,
            IHttpFetcher<long, Data.Models.Status> fetcher,
            ITypeMapper<Data.Models.Status, Status> mapper)
        {
            _session = session;
            _fetcher = fetcher;
            _mapper = mapper;
        }

        // GET api/<controller>
        public IEnumerable<Status> Get()
        {
            return _session
                .QueryOver<Data.Models.Status>()
                .List()
                .Select(_mapper.Create);
        }

        // GET api/<controller>/5
        public Status Get(long id)
        {
            Data.Models.Status p = _fetcher.GetModel(id);
            return _mapper.Create(p);
        }
    }
}