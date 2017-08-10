using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ISession _session;
        private readonly IHttpFetcher<long, Data.Models.Category> _fetcher;
        private readonly ITypeMapper<Data.Models.Category, Category> _mapper;

        public CategoriesController(
            ISession session,
            IHttpFetcher<long, Data.Models.Category> fetcher,
            ITypeMapper<Data.Models.Category, Category> mapper)
        {
            _session = session;
            _fetcher = fetcher;
            _mapper = mapper;
        }

        // GET: api/Categories
        public IEnumerable<Category> Get()
        {
            return _session
                .QueryOver<Data.Models.Category>()
                .List()
                .Select(_mapper.Create);
        }

        // GET: api/Categories/5
        public Category Get(long id)
        {
            Data.Models.Category p = _fetcher.GetModel(id);
            return _mapper.Create(p);
        }

        // POST: api/Categories
        [Authorize(Roles="Administrators")]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]Category value)
        {
            Data.Models.Category category = new Data.Models.Category
            {
                Description = value.Description,
                Name = value.Name
            };

            _session.Save(category);

            return request.CreatePostResponse(_mapper.Create(category));
        }

        // PUT: api/Categories
        [Authorize(Roles = "Administrators")]
        public void Put([FromBody]Category[] values)
        {
            Delete();

            foreach (Category c in values)
            {
                _session.Save(new Data.Models.Category
                    {
                        Description = c.Description,
                        Name = c.Name
                    });
            }
        }

        // PUT: api/Categories/5
        [Authorize(Roles = "Administrators")]
        public Category Put(HttpRequestMessage request, long id, [FromBody]Category value)
        {
            Data.Models.Category category = _fetcher.GetModel(id);

            category.Name = value.Name;
            category.Description = value.Description;

            _session.Update(category);

            return _mapper.Create(category);
        }

        // DELETE: api/Categories
        [Authorize(Roles = "Administrators")]
        public void Delete()
        {
            foreach (Category category in _session.QueryOver<Category>().List())
            {
                _session.Delete(category);
            }
        }

        // DELETE: api/Categories/5
        [Authorize(Roles = "Administrators")]
        public HttpResponseMessage Delete(long id)
        {
            Data.Models.Category category = _session.Get<Data.Models.Category>(id);

            if (category != null)
            {
                _session.Delete(category);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
