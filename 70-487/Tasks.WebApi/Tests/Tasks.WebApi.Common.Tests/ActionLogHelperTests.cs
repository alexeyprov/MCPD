using System;
using System.Web.Http.Controllers;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common.Tests
{
    [TestClass]
    public class ActionLogHelperTests
    {
        private Mock<ILog> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILog>();
            _loggerMock.Setup(x => x.IsDebugEnabled).Returns(true);
        }

        [TestMethod]
        public void LogEntry_should_log_with_format_string()
        {
            IActionLogHelper helper = new ActionLogHelper(_loggerMock.Object);

            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor
            {
                ControllerType = typeof(ActionLogHelper)
            };
            HttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor
            {
                ControllerDescriptor = controllerDescriptor
            };

            helper.LogEntry(actionDescriptor);

            _loggerMock.Verify(
                x => x.DebugFormat(
                    It.IsAny<string>(),
                    "ENTERING",
                    typeof(ActionLogHelper).FullName,
                    null));
        }

        [TestMethod]
        public void LogExit_should_log_with_format_string()
        {
            IActionLogHelper helper = new ActionLogHelper(_loggerMock.Object);

            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor
            {
                ControllerType = typeof(ActionLogHelper)
            };
            HttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor
            {
                ControllerDescriptor = controllerDescriptor
            };

            helper.LogExit(actionDescriptor);

            _loggerMock.Verify(
                x => x.DebugFormat(
                    It.IsAny<string>(),
                    "EXITING",
                    typeof(ActionLogHelper).FullName,
                    null));
        }
    }
}
