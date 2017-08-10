using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using CasinoSolution.Repository;
using MSDNEF6Article.DataLayer;
using DomainClasses;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomatedTests
{
  [TestClass]
  public class UnitTest1
  {




    [TestMethod, TestCategory("Pluralization")]
    public void PluralizationService_pluralize_names_taken_from_DbSet_property_type_names()
    {
      using (var context = new CasinoSlotsModel())
      {
        Assert.AreEqual("Casinos", UnitTestHelpers.GetEntitySetTableName(context, typeof(Casino)));
        Assert.AreEqual("Statii", UnitTestHelpers.GetEntitySetTableName(context, typeof(Status)));
      }
    }

    [TestMethod, TestCategory("Pluralization")]
    public void PluralizationService_CreatesCorrectNamesInDatabaseForCodeFirst()
    {
      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CasinoSlotsModel>());
      string sql =
        "select @tablecount=count(table_name) from INFORMATION_SCHEMA.Tables where table_name=@tablename";

      using (var context = new CasinoSlotsModel())
      {
        SqlParameter p = UnitTestHelpers.CountDbTables(context, sql, "Casinos");
        Assert.AreEqual(1, p.Value, "Casino");
        p = UnitTestHelpers.CountDbTables(context, sql, "Statii");
        Assert.AreEqual(1, p.Value, "Status");
        p = UnitTestHelpers.CountDbTables(context, sql, "FrenchJournaux");
        Assert.AreEqual(1, p.Value, "FrenchJournal");
      }
    }


  
  }
}