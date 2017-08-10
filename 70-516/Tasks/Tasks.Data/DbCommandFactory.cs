using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Tasks.Data.Interface;

namespace Tasks.Data
{
    public sealed class DbCommandFactory : IDbCommandFactory
    {
        private readonly ISession _session;

        public DbCommandFactory(ISession session)
        {
            _session = session;
        }

        IDbCommand IDbCommandFactory.GetCommand()
        {
            IDbConnection connection = GetOpenConnection();
            IDbCommand command = connection.CreateCommand();

            if (_session.Transaction != null)
            {
                _session.Transaction.Enlist(command);
            }

            return command;
        }

        private IDbConnection GetOpenConnection()
        {
            IDbConnection connection = _session.Connection;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}
