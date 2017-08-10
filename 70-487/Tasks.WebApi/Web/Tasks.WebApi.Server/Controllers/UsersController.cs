using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using NHibernate;
using NHibernate.Linq;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class UsersController : ApiController
    {
        private readonly ISession _session;
        private readonly IHttpFetcher<Guid, Data.Models.User> _fetcher;
        private readonly IUserMapper _mapper;
        private readonly IUserManager _userManager;

        public UsersController(
            ISession session,
            IHttpFetcher<Guid, Data.Models.User> fetcher,
            IUserMapper mapper,
            IUserManager userManager)
        {
            _session = session;
            _fetcher = fetcher;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET api/Users
        [EnableQuery]
        [OverrideActionFilters]
        public IQueryable<Data.Models.User> Get()
        {
            return _session.Query<Data.Models.User>();
        }

        // GET api/Users/xxxxxxxx-xxxx-....
        public User Get(Guid id)
        {
            Data.Models.User p = _fetcher.GetModel(id);
            return _mapper.Create(p);
        }

        // POST api/Users
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] User user)
        {
            User newUser = _userManager.CreateUser(
                user.UserName,
                user.Password,
                user.FirstName,
                user.LastName,
                user.Email);

            return request.CreatePostResponse(newUser);
        }
    }
}
