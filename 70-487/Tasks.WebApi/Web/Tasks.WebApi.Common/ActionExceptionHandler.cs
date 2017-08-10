using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using log4net;
using Tasks.Common.Interface;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common
{
    public sealed class ActionExceptionHandler : IActionExceptionHandler
    {
        private const int MAX_STATUS_LENGTH = 0x200;

        private readonly IExceptionMessageFormatter _exceptionFormatter;
        private readonly ILog _logger;

        public ActionExceptionHandler(IExceptionMessageFormatter formatter, ILog logger)
        {
            _exceptionFormatter = formatter;
            _logger = logger;
        }

        #region IActionExceptionHandler Members

        void IActionExceptionHandler.HandleException(HttpActionExecutedContext actionContext)
        {
            Exception exception = actionContext.Exception;
            if (exception == null)
            {
                return;
            }

            if (_logger.IsErrorEnabled)
            {
                _logger.Error("Unhandled API controller exception", exception);
            }

            string errorMessage = _exceptionFormatter.GetEntireExceptionStack(exception);
            errorMessage = errorMessage.Replace(Environment.NewLine, " ");

            if (errorMessage.Length > MAX_STATUS_LENGTH)
            {
                errorMessage = errorMessage.Substring(0, MAX_STATUS_LENGTH);
            }

            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = errorMessage
            };
        }

        #endregion
    }
}
