using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.WebApi.Server.Fetchers
{
    public interface IHttpFetcher<TKey, TModel>
    {
        TModel GetModel(TKey key);
    }
}
