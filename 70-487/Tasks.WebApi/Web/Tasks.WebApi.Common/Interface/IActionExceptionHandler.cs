using System.Web.Http.Filters;

namespace Tasks.WebApi.Common.Interface
{
    public interface IActionExceptionHandler
    {
        void HandleException(HttpActionExecutedContext filterContext);
    }
}