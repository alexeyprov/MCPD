using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject.Web.WebApi;

namespace Tasks.WebApi.Common
{
    public static class WebDependencyResolver
    {
        public static T Get<T>()
        {
            NinjectDependencyResolver dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver
                as NinjectDependencyResolver;

            if (dependencyResolver == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "{0} must be set as Web API dependency resolver",
                        typeof(NinjectDependencyResolver)));
            }

            return (T)dependencyResolver.GetService(typeof(T));
        }
    }
}
