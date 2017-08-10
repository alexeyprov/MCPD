using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using NHibernate;
using Tasks.Data.Models;

namespace Tasks.WebApi.Server.Fetchers
{
    internal sealed class HttpFetcher<TKey, TModel> : IHttpFetcher<TKey, TModel>
        where TModel : class
    {
        private readonly ISession _session;

        public HttpFetcher(ISession session)
        {
            _session = session;
        }

        #region IHttpFetcher<TKey, TModel> Members

        TModel IHttpFetcher<TKey, TModel>.GetModel(TKey key)
        {
            TModel result = _session.Get<TModel>(key);

            if (result == null)
            {
                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        ReasonPhrase = string.Format(
                            "{0} {1} not found",
                            typeof(TModel),
                            key)
                    });
            }

            return result;
        }

        #endregion
    }
}