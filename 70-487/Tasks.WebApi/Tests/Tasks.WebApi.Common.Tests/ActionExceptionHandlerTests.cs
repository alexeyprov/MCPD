using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasks.Common.Interface;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common.Tests
{
    [TestClass]
    public class ActionExceptionHandlerTests
    {
        private Mock<ILog> _loggerMock;
        private Mock<IExceptionMessageFormatter> _exceptionFormatterMock;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILog>(MockBehavior.Strict);
            _exceptionFormatterMock = new Mock<IExceptionMessageFormatter>(MockBehavior.Strict);
        }

        [TestMethod]
        public void HandleException_does_nothing_if_no_exception()
        {
            IActionExceptionHandler handler = new ActionExceptionHandler(_exceptionFormatterMock.Object, _loggerMock.Object);

            HttpActionExecutedContext context = new HttpActionExecutedContext
            {
                Exception = null
            };

            handler.HandleException(context);

            Assert.IsNull(context.Response);
        }

        [TestMethod]
        public void HandleException_exception_logged()
        {
            IActionExceptionHandler handler = new ActionExceptionHandler(_exceptionFormatterMock.Object, _loggerMock.Object);


            const string reasonPhrase = "dumb thing don't work";
            Exception exception = new Exception(reasonPhrase);

            HttpActionContext actionContext = new HttpActionContext
            {
                Response = new HttpResponseMessage()
            };
            HttpActionExecutedContext context = new HttpActionExecutedContext(actionContext, exception);

            _loggerMock
                .Setup(x => x.Error(It.IsAny<string>(), exception));
            _loggerMock
                .Setup(x => x.IsErrorEnabled)
                .Returns(true);

            _exceptionFormatterMock
                .Setup(x => x.GetEntireExceptionStack(exception))
                .Returns(reasonPhrase);

            handler.HandleException(context);

            _loggerMock.VerifyAll();
        }

        [TestMethod]
        public void HandleException_trims_message_if_too_long()
        {
            IActionExceptionHandler handler = new ActionExceptionHandler(_exceptionFormatterMock.Object, _loggerMock.Object);

            string reasonPhrase = new String('z', 0x200);
            reasonPhrase += "a";

            Exception exception = new Exception(reasonPhrase);

            HttpActionContext actionContext = new HttpActionContext
            {
                Response = new HttpResponseMessage()
            };
            HttpActionExecutedContext context = new HttpActionExecutedContext(actionContext, exception);

            _loggerMock
                .Setup(x => x.Error(It.IsAny<string>(), exception));
            _loggerMock
                .Setup(x => x.IsErrorEnabled)
                .Returns(true);

            _exceptionFormatterMock
                .Setup(x => x.GetEntireExceptionStack(exception))
                .Returns(reasonPhrase);

            handler.HandleException(context);

            string actualReasonPhrase = actionContext.Response.ReasonPhrase;
            Assert.AreEqual(0x200, actualReasonPhrase.Length);
            Assert.IsFalse(actualReasonPhrase.Contains("a"));
        }

        [TestMethod]
        public void HandleException_removes_newline()
        {
            IActionExceptionHandler handler = new ActionExceptionHandler(_exceptionFormatterMock.Object, _loggerMock.Object);

            string reasonPhrase = "one" + Environment.NewLine + "two";

            Exception exception = new Exception(reasonPhrase);

            HttpActionContext actionContext = new HttpActionContext
            {
                Response = new HttpResponseMessage()
            };
            HttpActionExecutedContext context = new HttpActionExecutedContext(actionContext, exception);

            _loggerMock
                .Setup(x => x.Error(It.IsAny<string>(), exception));
            _loggerMock
                .Setup(x => x.IsErrorEnabled)
                .Returns(true);

            _exceptionFormatterMock
                .Setup(x => x.GetEntireExceptionStack(exception))
                .Returns(reasonPhrase);

            handler.HandleException(context);

            string actualReasonPhrase = actionContext.Response.ReasonPhrase;
            Assert.AreEqual(actualReasonPhrase, "one two");
        }
    }
}
