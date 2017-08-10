using System.Web.Http.Controllers;

namespace Tasks.WebApi.Common.Interface
{
    public interface IActionLogHelper
    {
        void LogEntry(HttpActionDescriptor actionDescriptor);

        void LogExit(HttpActionDescriptor actionDescriptor);
    }
}