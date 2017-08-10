using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NHibernate;
using NHibernate.Linq;
using Tasks.Common.Interface;
using Tasks.Data.Models;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public abstract class TaskAssociationsController<TModel, TDataModel, TKey> : ApiController
        where TModel : IKeyedEntity<TKey>
        where TDataModel : IKeyedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IHttpFetcher<long, Task> _taskFetcher;
        private readonly IHttpFetcher<TKey, TDataModel> _childFetcher;
        private readonly ITypeMapper<TDataModel, TModel> _mapper;
        private readonly ISession _session;
        private readonly Func<Task, ICollection<TDataModel>> _childrenSelector;

        public TaskAssociationsController(
            IHttpFetcher<long, Task> taskFetcher,
            IHttpFetcher<TKey, TDataModel> childFetcher,
            ITypeMapper<TDataModel, TModel> mapper,
            ISession session,
            Func<Task, ICollection<TDataModel>> childrenSelector)
        {
            _taskFetcher = taskFetcher;
            _childFetcher = childFetcher;
            _mapper = mapper;
            _session = session;
            _childrenSelector = childrenSelector;
        }

        // GET api/tasks/5/<children>
        public virtual IEnumerable<TModel> Get(long taskId)
        {
            Task task = _taskFetcher.GetModel(taskId);

            return _childrenSelector(task)
                .Select(u => _mapper.Create(u))
                .ToArray();
        }

        // PUT api/tasks/5/<children>/xxxxxxxx-xxxx-...
        public virtual void Put(long taskId, TKey id)
        {
            Task task = _taskFetcher.GetModel(taskId);

            if (!_childrenSelector(task).Any(u => u.Id.Equals(id)))
            {
                _childrenSelector(task).Add(_childFetcher.GetModel(id));
                _session.Update(task);
            }
        }

        // PUT api/tasks/5/<children>
        public virtual void Put(long taskId, [FromBody] IEnumerable<TModel> values)
        {
            Task task = _taskFetcher.GetModel(taskId);

            ICollection<TKey> newIds = new HashSet<TKey>(values.Select(u => u.Id));
            ICollection<TKey> existingIds = new HashSet<TKey>();
            ICollection<TDataModel> children = _childrenSelector(task);
            IEnumerable<TDataModel> existingChildren = new List<TDataModel>(children);

            foreach (TDataModel child in existingChildren)
            {
                if (newIds.Contains(child.Id))
                {
                    newIds.Remove(child.Id);
                }
                else
                {
                    children.Remove(child);
                }
            }

            foreach (TDataModel child in _session
                .Query<TDataModel>()
                .Where(u => newIds.Contains(u.Id)))
            {
                children.Add(child);
            }

            _session.Update(task);
        }

        // DELETE api/tasks/5/<children>/xxxxxxxx-xxxx-...
        public virtual void Delete(long taskId, TKey id)
        {
            Task task = _taskFetcher.GetModel(taskId);
            TDataModel child = _childrenSelector(task).SingleOrDefault(u => u.Id.Equals(id));

            if (child == null)
            {
                return;
            }

            _childrenSelector(task).Remove(child);
            _session.Update(task);
        }

        // DELETE api/tasks/5/<children>
        public virtual void Delete(long taskId)
        {
            Task task = _taskFetcher.GetModel(taskId);

            _childrenSelector(task).Clear();
            _session.Update(task);
        }
    }
}
