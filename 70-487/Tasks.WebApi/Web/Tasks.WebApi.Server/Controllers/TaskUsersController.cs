using System;
using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class TaskUsersController : TaskAssociationsController<User, Data.Models.User, Guid>
    {
        public TaskUsersController(
            IHttpFetcher<long, Data.Models.Task> taskFetcher,
            IHttpFetcher<Guid, Data.Models.User> userFetcher,
            IUserMapper mapper,
            ISession session) : 
            base(taskFetcher, userFetcher, mapper, session, t => t.Assignees)
        {
        }
    }
}