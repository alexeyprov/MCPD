using System;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.Web.WebApi;

namespace Tasks.WebApi.Common.Tests
{
    [TestClass]
    public class WebDependencyResolverTests
    {
        public interface ITestableInterface
        {
        }

        public class TestableClass : ITestableInterface
        {
        }

        [TestMethod]
        public void Get_Throws_with_Mock_resolver()
        {
            Mock<IDependencyResolver> mockDependencyResolver = new Mock<IDependencyResolver>();
            mockDependencyResolver.Setup(x => x.GetService(typeof(ITestableInterface))).Returns(new TestableClass());
            GlobalConfiguration.Configuration.DependencyResolver = mockDependencyResolver.Object;

            try
            {
                WebDependencyResolver.Get<ITestableInterface>();
                Assert.Fail("InvalidOperationException should have been thrown");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
        public void Get_Returns_Instance_Of_Class()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ITestableInterface>().To<TestableClass>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            ITestableInterface testable = WebDependencyResolver.Get<ITestableInterface>();
            Assert.IsInstanceOfType(testable, typeof(TestableClass));
        }
    }
}
