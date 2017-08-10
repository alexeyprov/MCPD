using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Tasks.Data.Interface;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        private readonly IDbCommandFactory _commandFactory;

        public UserRepository(ISession session, IDbCommandFactory commandFactory)
        {
            _session = session;
            _commandFactory = commandFactory;
        }

        public User GetUser(Guid userId)
        {
            return _session.Get<User>(userId);
        }

        public void SaveUser(Guid userId, string firstname, string lastname)
        {
            using (IDbCommand command = _commandFactory.GetCommand())
            {
                command.CommandText = "dbo.SaveUser";
                command.CommandType = CommandType.StoredProcedure;

                AddParameter(command, "@userId", userId);
                AddParameter(command, "@firstname", firstname);
                AddParameter(command, "@lastname", lastname);

                command.ExecuteNonQuery();
            }
        }

        private static void AddParameter(IDbCommand command, string name, object value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            command.Parameters.Add(parameter);
        }
    }

}
