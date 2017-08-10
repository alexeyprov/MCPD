using System.Web.Http.Filters;
using NHibernate;
using NHibernate.Context;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common
{
    public sealed class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly ISessionFactory _sessionFactory;

        public ActionTransactionHelper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region IActionTransactionHelper Members

        void IActionTransactionHelper.BeginTransaction()
        {
            ISession session = GetSession();

            if (session != null)
            {
                session.BeginTransaction();
            }
        }

        void IActionTransactionHelper.EndTransaction(HttpActionExecutedContext actionExecutedContext)
        {
            ISession session = GetSession();

            if (session != null && session.Transaction.IsActive)
            {
                if (actionExecutedContext.Exception == null)
                {
                    session.Flush();
                    session.Transaction.Commit();
                }
                else
                {
                    session.Transaction.Rollback();
                }
            }
        }

        void IActionTransactionHelper.CloseSession()
        {
            if (CurrentSessionContext.HasBind(_sessionFactory))
            {
                ISession session = _sessionFactory.GetCurrentSession();

                session.Close();
                session.Dispose();

                CurrentSessionContext.Unbind(_sessionFactory);
            }
        }

        #endregion

        #region Implementation

        private ISession GetSession()
        {
            ISession session = null;
            try
            {
                session = _sessionFactory.GetCurrentSession();
            }
            catch (HibernateException)
            {
            }
            return session;
        } 
        
        #endregion
    }
}
