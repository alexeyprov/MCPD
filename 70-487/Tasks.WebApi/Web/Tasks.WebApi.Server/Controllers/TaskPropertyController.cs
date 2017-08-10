using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NHibernate;
using Tasks.Common.Interface;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server.Controllers
{
    public abstract class TaskPropertyController<TModel, TDataModel> : ApiController
        where TModel : IKeyedEntity<long>
        where TDataModel : IKeyedEntity<long>
    {
        private readonly IHttpFetcher<long, Data.Models.Task> _taskFetcher;
        private readonly ITypeMapper<TDataModel, TModel> _mapper;
        private readonly IHttpFetcher<long, TDataModel> _fetcher;
        private readonly ISession _session;

        public TaskPropertyController(
            IHttpFetcher<long, Data.Models.Task> taskFetcher,
            IHttpFetcher<long, TDataModel> fetcher,
            ITypeMapper<TDataModel, TModel> mapper,
            ISession session)
        {
            _taskFetcher = taskFetcher;
            _fetcher = fetcher;
            _mapper = mapper;
            _session = session;
        }

        // GET api/tasks/5/<property>
        public virtual TModel Get(long taskId)
        {
            Data.Models.Task task = _taskFetcher.GetModel(taskId);
            return _mapper.Create(GetProperty(task));
        }

        // PUT api/tasks/5/<property>/7
        public virtual void Put(long taskId, long id)
        {
            Data.Models.Task task = _taskFetcher.GetModel(taskId);
            TDataModel prop = _fetcher.GetModel(id);

            SetProperty(task, prop);
            _session.Save(task);
        }

        protected abstract TDataModel GetProperty(Data.Models.Task task);

        protected abstract void SetProperty(Data.Models.Task task, TDataModel value);
    }
}