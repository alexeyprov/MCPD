using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks.Common.Interface;

namespace Tasks.Common.Tests
{
    [TestClass]
    public class ExceptionMessageFormatterTests
    {
        [TestMethod]
        public void GetEntireExceptionStack_returns_single_message_when_only_one_exception()
        {
            IExceptionMessageFormatter formatter = new ExceptionMessageFormatter();

            Exception exception = new Exception("test");

            string result = formatter.GetEntireExceptionStack(exception);

            Assert.AreEqual(exception.Message, result);
        }

        [TestMethod]
        public void GetEntireExceptionStack_returns_all_messages_when_three_exceptions()
        {
            IExceptionMessageFormatter formatter = new ExceptionMessageFormatter();

            Exception exception3 = new Exception("three");
            Exception exception2 = new Exception("two", exception3);
            Exception exception1 = new Exception("one", exception2);

            string result = formatter.GetEntireExceptionStack(exception1);

            Assert.AreEqual("one --> two --> three", result);
        }
    }
}
