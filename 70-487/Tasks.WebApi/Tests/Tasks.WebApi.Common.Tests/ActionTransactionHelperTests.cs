using System;
using System.Web.Http.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common.Tests
{
    [TestClass]
    public class ActionTransactionHelperTests
    {
        private Mock<ISessionFactory> _sessionFactoryMock;
        private Mock<ISession> _sessionMock;
        private Mock<ITransaction> _transactionMock;

        [TestInitialize]
        public void Initialize()
        {
            _sessionFactoryMock = new Mock<ISessionFactory>(MockBehavior.Strict);
            _sessionMock = new Mock<ISession>(MockBehavior.Strict);
            _transactionMock = new Mock<ITransaction>(MockBehavior.Strict);
        }

        [TestMethod]
        public void BeginTransaction_starts_transaction_on_session()
        {
            IActionTransactionHelper helper = CreateHelper();

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(_sessionMock.Object);

            _sessionMock
                .Setup(x => x.BeginTransaction())
                .Returns(_transactionMock.Object);

            helper.BeginTransaction();

            _sessionMock.VerifyAll();
        }

        private IActionTransactionHelper CreateHelper()
        {
            return new ActionTransactionHelper(_sessionFactoryMock.Object);
        }

        [TestMethod]
        public void BeginTransaction_does_not_blow_it_no_active_session()
        {
            IActionTransactionHelper helper = CreateHelper();

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(value: null);

           helper.BeginTransaction();
        }

        [TestMethod]
        public void EndTransaction_transaction_not_handled_when_no_active_session()
        {
            IActionTransactionHelper helper = CreateHelper();

            HttpActionExecutedContext context = new HttpActionExecutedContext();

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(value: null);

            helper.EndTransaction(context);
        }

        [TestMethod]
        public void EndTransaction_transaction_not_handled_when_no_active_transaction()
        {
            IActionTransactionHelper helper = CreateHelper();

            HttpActionExecutedContext context = new HttpActionExecutedContext();

            Mock<ITransaction> transactionMock = new Mock<ITransaction>();
            transactionMock
                .Setup(x => x.IsActive)
                .Returns(false);

            _sessionMock
                .Setup(x => x.Transaction)
                .Returns(transactionMock.Object);

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(_sessionMock.Object);

            helper.EndTransaction(context);
        }

        [TestMethod]
        public void EndTransaction_flush_and_commit_when_no_exception()
        {
            IActionTransactionHelper helper = CreateHelper();

            HttpActionExecutedContext context = new HttpActionExecutedContext();

            _transactionMock
                .Setup(x => x.IsActive)
                .Returns(true);

            _sessionMock
                .Setup(x => x.Transaction)
                .Returns(_transactionMock.Object);

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(_sessionMock.Object);

            _sessionMock.Setup(x => x.Flush());

            _transactionMock.Setup(x => x.Commit());

            helper.EndTransaction(context);

            _sessionMock.VerifyAll();
            _transactionMock.VerifyAll();
        }

        [TestMethod]
        public void EndTransaction_rollback_when_exception_exists()
        {
            IActionTransactionHelper helper = CreateHelper();

            HttpActionExecutedContext context = new HttpActionExecutedContext
            {
                Exception = new Exception()
            };

            Mock<ITransaction> transactionMock = new Mock<ITransaction>();
            transactionMock
                .Setup(x => x.IsActive)
                .Returns(true);

            _sessionMock
                .Setup(x => x.Transaction)
                .Returns(transactionMock.Object);

            _sessionFactoryMock
                .Setup(x => x.GetCurrentSession())
                .Returns(_sessionMock.Object);

            helper.EndTransaction(context);

            transactionMock.Verify(x => x.Rollback());
        }
    }
}
