using System;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Tasks.Common;
using Tasks.Common.Interface;
using Tasks.Data;
using Tasks.Data.Interface;
using Tasks.Data.Models;
using Tasks.Data.SqlServer;
using Tasks.WebApi.Common;
using Tasks.WebApi.Common.Interface;
using Tasks.WebApi.Common.Security;
using Tasks.WebApi.Server.Fetchers;
using Tasks.WebApi.Server.Mappers;


namespace Tasks.WebApi.Server.App_Start
{
    /// <summary>
    /// Class used to set up the Ninject DI container.
    /// </summary>
    internal sealed class NinjectConfigurator
    {
        private readonly IKernel _container;

        public NinjectConfigurator(IKernel container)
        {
            _container = container;
        }

        /// <summary>
        /// Entry method used by caller to configure the given 
        /// container with all of this application's 
        /// dependencies. Also configures the container as this
        /// application's dependency resolver.
        /// </summary>
        public void Configure()
        {
            // add all bindings
            AddBindings();

            // set up dependency resolver for Web API
            //GlobalConfiguration.Configuration.DependencyResolver =
            //    new NinjectDependencyResolver(_container);
        }

        ///<summary>
        /// Add all bindings/dependencies to the container
        ///</summary>
        private void AddBindings()
        {
            ConfigureNhibernate();

            ConfigureLog4net();

            // ORM proxy components
            _container.Bind<IHttpFetcher<long, Category>>().To<HttpFetcher<long, Category>>();
            _container.Bind<IHttpFetcher<long, Priority>>().To<HttpFetcher<long, Priority>>();
            _container.Bind<IHttpFetcher<long, Status>>().To<HttpFetcher<long, Status>>();
            _container.Bind<IHttpFetcher<long, Task>>().To<HttpFetcher<long, Task>>();
            _container.Bind<IHttpFetcher<Guid, User>>().To<HttpFetcher<Guid, User>>();

            // data model to web api model mapping components
            _container.Bind<ITypeMapper<Category, WebApi.Models.Category>>().To<CategoryMapper>();
            _container.Bind<ITypeMapper<Priority, WebApi.Models.Priority>>().To<PriorityMapper>();
            _container.Bind<ITypeMapper<Status, WebApi.Models.Status>>().To<StatusMapper>();
            _container.Bind<IUserMapper>().To<UserMapper>();
            _container.Bind<ITypeMapper<Task, WebApi.Models.Task>>().To<TaskMapper>();

            // user management components
            _container.Bind<IUserManager>().To<UserManager>();
            _container.Bind<IMembershipInfoProvider>().To<MembershipAdapter>();
            _container.Bind<IUserSession>().ToMethod(CreateUserSession).InRequestScope();

            // data access components
            _container.Bind<IDbCommandFactory>().To<DbCommandFactory>();
            _container.Bind<IUserRepository>().To<UserRepository>();
            _container.Bind<IDatabaseValueParser>().To<DatabaseValueParser>();

            // web api helpers
            _container.Bind<IActionLogHelper>().To<ActionLogHelper>();
            _container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>();
            _container.Bind<IActionExceptionHandler>().To<ActionExceptionHandler>();

            // other helpers
            _container.Bind<IDateTime>().To<DateTimeAdapter>();
            _container.Bind<IExceptionMessageFormatter>().To<ExceptionMessageFormatter>();
        }

        ///<summary>
        /// Set up log4net for this application, including putting it in the
        /// given container.
        ///</summary>
        private void ConfigureLog4net()
        {
            XmlConfigurator.Configure();

            _container.Bind<ILog>().ToConstant(
                LogManager.GetLogger("TasksWebApiServer"));
        }

        ///<summary>
        /// Sets up NHibernate, and adds an ISessionFactory to the given
        /// container.
        ///</summary>
        private void ConfigureNhibernate()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(
                        b => b.FromConnectionStringWithKey("Local")))
                .CurrentSessionContext("web")
                .Mappings(
                    c => c.FluentMappings.AddFromAssemblyOf<UserRepository>())
                .BuildSessionFactory();

            _container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            _container.Bind<ISession>().ToMethod(CreateNhibernateSession);
        }

        ///<summary>
        /// Used to fetch the current thread's principal as
        /// an <see cref="IUserSession"/> object.
        ///</summary>
        private static IUserSession CreateUserSession(IContext ctx)
        {
            return new UserSession(Thread.CurrentPrincipal as ClaimsPrincipal);
        }

        ///<summary>
        /// Method used to create instances of ISession objects
        /// and bind them to the HTTP context.
        ///</summary>
        private static ISession CreateNhibernateSession(IContext ctx)
        {
            ISessionFactory factory = ctx.Kernel.Get<ISessionFactory>();

            if (!CurrentSessionContext.HasBind(factory))
            {
                ISession session = factory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return factory.GetCurrentSession();
        }
    }
}