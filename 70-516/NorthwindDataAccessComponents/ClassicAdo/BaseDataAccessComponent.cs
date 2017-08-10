using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using ReflectionUtilities;

namespace Northwind.Data.ClassicAdo
{
    public abstract class BaseDataAccessComponent
    {
        #region Private Constants

        private const string DERIVE_PARAMS_METHOD = "DeriveParameters";

        private const string ASC_SORTING = "ASC";
        private const string DESC_SORTING = "DESC";

        #endregion

        #region Data Members

        private string _connectionString;
        private static DbProviderFactory _factory;
        private static MethodInfo _deriveParametersMethod;
        private static IDictionary<string, IDbDataParameter[]> _parameterCache;
        private static int _paramStartIndex;

        #endregion

        #region Construction

        static BaseDataAccessComponent()
        {
            _parameterCache = new Dictionary<string, IDbDataParameter[]>();
        }

        protected BaseDataAccessComponent()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
        }

        protected BaseDataAccessComponent(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        public static void Initialize(string providerName, int startParamIndex)
        {
            if (null == _factory)
            {
                lock (typeof(BaseDataAccessComponent))
                {
                    if (null == _factory)
                    {
                        _factory = DbProviderFactories.GetFactory(providerName);
                        using (DbCommandBuilder builder = _factory.CreateCommandBuilder())
                        {
                            _deriveParametersMethod = builder.GetType().GetMethod(DERIVE_PARAMS_METHOD,
                                BindingFlags.Public | BindingFlags.Static);
                        }

                        _paramStartIndex = startParamIndex;
                    }
                }
            }
        }

        #region Implementation

        protected DbConnection GetConnection()
        {
            return GetConnection(false);
        }

        protected DbConnection GetConnection(bool open)
        {
            DbConnection cn = _factory.CreateConnection();
            cn.ConnectionString = _connectionString;

            if (open)
            {
                cn.Open();
            }

            return cn;
        }

        protected DataContext GetDataContext()
        {
            return new DataContext(_connectionString);
        }

        protected DbCommand GetStoredProcCommand(string text, params object[] pars)
        {
            DbCommand cmd = CreateStoredProcCommand(text);

            FillParameters(cmd);

            for (int i = 0; i < pars.Length; ++i)
            {
                cmd.Parameters[i + _paramStartIndex].Value = pars[i];
            }

            return cmd;
        }

        protected DbDataReader ExecuteReader(DbCommand cmd)
        {
            DbConnection cn = null;
            try
            {
                cn = GetConnection(true);
                cmd.Connection = cn;

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (cn != null)
                {
                    cn.Close();
                }
                throw;
            }
        }

        protected void ExecuteNonQuery(DbCommand cmd)
        {
            using (DbConnection cn = GetConnection(true))
            {
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        protected void Sort<T>(List<T> array, string sortExpression)
        {
            if (String.IsNullOrEmpty(sortExpression))
            {
                return;
            }

            bool needReverse = false;
            if (sortExpression.EndsWith(ASC_SORTING))
            {
                sortExpression = sortExpression.Substring(0, sortExpression.Length - ASC_SORTING.Length).Trim();
            }
            else if (sortExpression.EndsWith(DESC_SORTING))
            {
                sortExpression = sortExpression.Substring(0, sortExpression.Length - DESC_SORTING.Length).Trim();
                needReverse = true;
            }

            array.Sort(new CompositePropertyComparer<T>(sortExpression));

            if (needReverse)
            {
                array.Reverse();
            }
        }

        private void FillParameters(DbCommand cmd)
        {
            IDbDataParameter[] pars;

            if (!_parameterCache.TryGetValue(cmd.CommandText, out pars))
            {
                //Derive and fill cache if needed
                DbCommand deriveCmd = CreateStoredProcCommand(cmd.CommandText);

                using (DbConnection cn = GetConnection(true))
                {
                    deriveCmd.Connection = cn;
                    DeriveParameters(deriveCmd);
                }

                pars = (from IDbDataParameter par in deriveCmd.Parameters
                        select (IDbDataParameter)((ICloneable)par).Clone()).ToArray();

                _parameterCache.Add(cmd.CommandText, pars);
            }

            foreach (IDbDataParameter par in pars)
            {
                cmd.Parameters.Add(((ICloneable)par).Clone());
            }
        }

        private void DeriveParameters(DbCommand deriveCmd)
        {
            _deriveParametersMethod.Invoke(null, new object[] { deriveCmd });
        }

        private DbCommand CreateStoredProcCommand(string commandText)
        {
            DbCommand cmd = _factory.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        #endregion
    }
}
