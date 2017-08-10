using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Web.WebApi;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common
{
    public class NhibernateSessionAttribute : ActionFilterAttribute
    {
        private readonly IActionLogHelper _logHelper;
        private readonly IActionTransactionHelper _transactionHelper;
        private readonly IActionExceptionHandler _exceptionHandler;

        public NhibernateSessionAttribute() :
            this(
                WebDependencyResolver.Get<IActionLogHelper>(),
                WebDependencyResolver.Get<IActionTransactionHelper>(),
                WebDependencyResolver.Get<IActionExceptionHandler>())
        {
        }

        public NhibernateSessionAttribute(
            IActionLogHelper logHelper,
            IActionTransactionHelper transactionHelper,
            IActionExceptionHandler exceptionHandler)
        {
            _logHelper = logHelper;
            _transactionHelper = transactionHelper;
            _exceptionHandler = exceptionHandler;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            _logHelper.LogEntry(actionContext.ActionDescriptor);
            _transactionHelper.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            _transactionHelper.EndTransaction(actionExecutedContext);
            _transactionHelper.CloseSession();

            _exceptionHandler.HandleException(actionExecutedContext);

            _logHelper.LogExit(actionExecutedContext.ActionContext.ActionDescriptor);
        }


    }
}
