using NHibernate;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class TaskStatusController : TaskPropertyController<Status, Data.Models.Status>
    {
        public TaskStatusController(
            IHttpFetcher<long, Data.Models.Task> taskFetcher,
            IHttpFetcher<long, Data.Models.Status> statusFetcher,
            ITypeMapper<Data.Models.Status, Status> mapper,
            ISession session) :
            base(taskFetcher, statusFetcher, mapper, session)
        {
        }

        protected override Data.Models.Status GetProperty(Data.Models.Task task)
        {
            return task.Status;
        }

        protected override void SetProperty(Data.Models.Task task, Data.Models.Status value)
        {
            task.Status = value;
        }
    }
}