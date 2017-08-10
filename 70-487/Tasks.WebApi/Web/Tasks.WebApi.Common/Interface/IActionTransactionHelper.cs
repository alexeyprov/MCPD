using System.Web.Http.Filters;

namespace Tasks.WebApi.Common.Interface
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();

        void EndTransaction(HttpActionExecutedContext filterContext);

        void CloseSession();
    }
}