using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Threading.Tasks;
using CasinoSolution.Repository;
using MSDNEF6Article.DataLayer;
using DomainClasses;

namespace AutomatedTests
{
    public static class UnitTestHelpers
    {
    
        internal static string GetEntitySetTableName(DbContext dbContext, Type clrType)
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;

            var container = objectContext.MetadataWorkspace
                .GetItems<EntityContainer>(DataSpace.SSpace)
                .SingleOrDefault();

            if (container == null)
            {
                return null;
            }

            var entitySet = container.BaseEntitySets
                .Where(bes => bes.ElementType.Name == clrType.Name)
                .SingleOrDefault();

            if (entitySet == null)
            {
                return null;
            }

            return entitySet.Table;
        }
        internal static SqlParameter CountDbTables(CasinoSlotsModel context, string sql, string name)
        {
            var p = new SqlParameter
            {
                ParameterName = "@tablecount",
                DbType = DbType.Int32,
                Size = 1,
                Direction = ParameterDirection.Output
            };
            context.Database.ExecuteSqlCommand(sql, new SqlParameter("@tableName", name), p);
            return p;
        }
    
    }
}