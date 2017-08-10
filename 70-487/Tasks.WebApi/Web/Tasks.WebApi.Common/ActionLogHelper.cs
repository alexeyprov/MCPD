using System.Web.Http.Controllers;
using log4net;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common
{
    public sealed class ActionLogHelper : IActionLogHelper
    {
        private readonly ILog _logger;

        public ActionLogHelper(ILog logger)
        {
            _logger = logger;
        }

        #region IActionLogHelper Members

        void IActionLogHelper.LogEntry(HttpActionDescriptor actionDescriptor)
        {
            LogAction(actionDescriptor, "ENTERING");
        }

        void IActionLogHelper.LogExit(HttpActionDescriptor actionDescriptor)
        {
            LogAction(actionDescriptor, "EXITING");
        }

        #endregion

        private void LogAction(HttpActionDescriptor actionDescriptor, string prefix)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.DebugFormat(
                    "{0} {1}::{2}",
                    prefix,
                    actionDescriptor.ControllerDescriptor.ControllerType.FullName,
                    actionDescriptor.ActionName);
            }
        }
    }
}
