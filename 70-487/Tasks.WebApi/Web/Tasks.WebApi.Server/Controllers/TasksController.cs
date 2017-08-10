using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NHibernate;
using NHibernate.Linq;

using Tasks.Common.Interface;
using Tasks.WebApi.Common.Interface;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public class TasksController : ApiController
    {
        private readonly IDateTime _dateTimeAdapter;
        private readonly ISession _session;
        private readonly IHttpFetcher<long, Data.Models.Task> _fetcher;
        private readonly ITypeMapper<Data.Models.Task, Task> _mapper;
        private readonly IUserSession _currentUser;

        public TasksController(
            IDateTime dateTimeAdapter,
            ISession session,
            IHttpFetcher<long, Data.Models.Task> fetcher,
            ITypeMapper<Data.Models.Task, Task> mapper,
            IUserSession currentUser)
        {
            _dateTimeAdapter = dateTimeAdapter;
            _session = session;
            _fetcher = fetcher;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        // GET: api/Tasks
        public IEnumerable<Task> Get()
        {
            return _session
                .Query<Data.Models.Task>()
                .Where(t => t.CreatedBy.UserId == _currentUser.UserId ||
                    t.Assignees.Any(a => a.UserId == _currentUser.UserId))
                .Select(_mapper.Create)
                .ToArray();
        }

        // GET: api/Tasks/5
        public Task Get(long id)
        {
            Data.Models.Task p = _fetcher.GetModel(id);
            return _mapper.Create(p);
        }

        // POST: api/Tasks
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]Task value)
        {
            Data.Models.Task task = new Data.Models.Task
            {
                CreatedBy = _session.Get<Data.Models.User>(_currentUser.UserId),
                CreatedDate = _dateTimeAdapter.UtcNow,
                DueDate = value.DueDate,
                Priority = _session.Get<Data.Models.Priority>(value.Priority.PriorityId),
                StartDate = value.StartDate,
                Status = _session.Get<Data.Models.Status>(value.Status.StatusId),
                Subject = value.Subject
            };

            FillDataModelCollection(task.Assignees, value.Assignees, u => u.UserId);
            FillDataModelCollection(task.Categories, value.Categories, c => c.CategoryId);

            _session.Save(task);

            return request.CreatePostResponse(_mapper.Create(task));
        }

        // PUT: api/Tasks/5
        public Task Put(HttpRequestMessage request, long id, [FromBody]Task value)
        {
            Data.Models.Task task = _fetcher.GetModel(id);

            task.DueDate = value.DueDate;
            task.StartDate = value.StartDate;
            task.Subject = value.Subject;

            _session.Update(task);

            return _mapper.Create(task);
        }

        // DELETE: api/Tasks/5
        public HttpResponseMessage Delete(long id)
        {
            Data.Models.Task task = _fetcher.GetModel(id);

            task.Status = _session.Query<Data.Models.Status>().Single(s => s.Name == "Completed");
            task.DateCompleted = _dateTimeAdapter.UtcNow;

            _session.Update(task);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private void FillDataModelCollection<TDataModel, TModel, TKey>(
            ICollection<TDataModel> dataModels,
            IEnumerable<TModel> models,
            Func<TModel, TKey> keySelector)
        {
            foreach (TModel model in models)
            {
                TKey key = keySelector(model);
                dataModels.Add(_session.Get<TDataModel>(key));
            }
        }
    }
}
