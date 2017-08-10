using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class TaskPriorityController : TaskPropertyController<Priority, Data.Models.Priority>
    {
        public TaskPriorityController(
            IHttpFetcher<long, Data.Models.Task> taskFetcher,
            IHttpFetcher<long, Data.Models.Priority> priorityFetcher,
            ITypeMapper<Data.Models.Priority, Priority> mapper,
            ISession session) :
            base(taskFetcher, priorityFetcher, mapper, session)
        {
        }

        protected override Data.Models.Priority GetProperty(Data.Models.Task task)
        {
            return task.Priority;
        }

        protected override void SetProperty(Data.Models.Task task, Data.Models.Priority value)
        {
            task.Priority = value;
        }
    }
}