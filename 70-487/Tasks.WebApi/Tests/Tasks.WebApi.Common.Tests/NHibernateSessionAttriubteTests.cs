using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasks.WebApi.Common.Interface;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Tasks.WebApi.Common.Tests
{
    /// <summary>
    /// Summary description for NHibernateSessionAttriubteTests
    /// </summary>
    [TestClass]
    public class NHibernateSessionAttriubteTests
    {
        private Mock<IActionLogHelper> _logHelperMock;
        private Mock<IActionTransactionHelper> _transactionHelperMock;
        private Mock<IActionExceptionHandler> _exceptionHandlerMock;


        #region Additional test attributes
        
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void Initialize()
        {
            _logHelperMock = new Mock<IActionLogHelper>(MockBehavior.Strict);
            _transactionHelperMock = new Mock<IActionTransactionHelper>(MockBehavior.Strict);
            _exceptionHandlerMock = new Mock<IActionExceptionHandler>(MockBehavior.Strict);
        }

        #endregion

        [TestMethod]
        public void OnActionExecuting_should_log_entry_and_start_transaction_in_order()
        {
            ActionFilterAttribute target = CreateTarget();
            HttpActionContext actionContext = new HttpActionContext
            {
                ActionDescriptor = new ReflectedHttpActionDescriptor()
            };

            int ordinal = 0;
            int loggerOrdinal = -1;
            int transactionOrdinal = -1;

            _logHelperMock
                .Setup(x => x.LogEntry(actionContext.ActionDescriptor))
                .Callback(() => loggerOrdinal = ordinal++);
            _transactionHelperMock
                .Setup(x => x.BeginTransaction())
                .Callback(() => transactionOrdinal = ordinal++);

            target.OnActionExecuting(actionContext);

            _logHelperMock.VerifyAll();
            _transactionHelperMock.VerifyAll();

            Assert.AreEqual(0, loggerOrdinal);
            Assert.AreEqual(1, transactionOrdinal);
        }

        [TestMethod]
        public void OnActionExecuted_should_end_transaction_log_exception_log_exit_in_order()
        {
            ActionFilterAttribute target = CreateTarget();
            HttpActionContext actionContext = new HttpActionContext
            {
                ActionDescriptor = new ReflectedHttpActionDescriptor()
            };
            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext
            {
                ActionContext = actionContext
            };

            int ordinal = 0;
            int loggerOrdinal = -1;
            int transactionOrdinal = -1;
            int sessionOrdinal = -1;
            int exceptionOrdinal = -1;

            _transactionHelperMock
                .Setup(x => x.EndTransaction(actionExecutedContext))
                .Callback(() => transactionOrdinal = ordinal++);
            _transactionHelperMock
                .Setup(x => x.CloseSession())
                .Callback(() => sessionOrdinal = ordinal++);
            _exceptionHandlerMock
                .Setup(x => x.HandleException(actionExecutedContext))
                .Callback(() => exceptionOrdinal = ordinal++);
            _logHelperMock
                .Setup(x => x.LogExit(actionContext.ActionDescriptor))
                .Callback(() => loggerOrdinal = ordinal++);

            target.OnActionExecuted(actionExecutedContext);

            _logHelperMock.VerifyAll();
            _transactionHelperMock.VerifyAll();
            _exceptionHandlerMock.VerifyAll();

            Assert.AreEqual(0, transactionOrdinal);
            Assert.AreEqual(1, sessionOrdinal);
            Assert.AreEqual(2, exceptionOrdinal);
            Assert.AreEqual(3, loggerOrdinal);
        }

        private NhibernateSessionAttribute CreateTarget()
        {
            return new NhibernateSessionAttribute(
                _logHelperMock.Object,
                _transactionHelperMock.Object,
                _exceptionHandlerMock.Object);
        }
    }
}
