using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.SqlServer;
using DataLayer.Pluralization;
using MSDNEF6Article.DataLayer;
using CasinoModel.Migrations;

//using System.Data.Entity.Config;

namespace DataLayer.DbConfigurations
{
public class CustomDbConfiguration : DbConfiguration
{
  public CustomDbConfiguration()
  {
    SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
    //SetDatabaseInitializer(new MigrateDatabaseToLatestVersion<CasinoSlotsModel, Configuration>());
    //DatabaseInitializer(new MyInitializer()); 
    SetHistoryContext("System.Data.SqlClient",
                (connection, defaultSchema) => new MyHistoryContext(connection, defaultSchema));
    SetPluralizationService(new CustomPluralizationService());
    SetMigrationSqlGenerator("System.Data.SqlClient", () => new CustomSqlServerMigrationSqlGenerator());
    
    //SetPluralizationService(new EnglishPluralizationService(
    //                          new CustomPluralizationEntry[]
    //                            {
    //                              new CustomPluralizationEntry("Rhinocerous", "Rhinocerii")
    //                            }));

  }
}
}
