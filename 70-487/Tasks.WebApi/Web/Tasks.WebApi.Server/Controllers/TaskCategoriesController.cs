using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class TaskCategoriesController : TaskAssociationsController<Category, Data.Models.Category, long>
    {
        public TaskCategoriesController(
            IHttpFetcher<long, Data.Models.Task> taskFetcher,
            IHttpFetcher<long, Data.Models.Category> categoryFetcher,
            ITypeMapper<Data.Models.Category, Category> mapper,
            ISession session) : 
            base(taskFetcher, categoryFetcher, mapper, session, t => t.Categories)
        {
        }
    }
}