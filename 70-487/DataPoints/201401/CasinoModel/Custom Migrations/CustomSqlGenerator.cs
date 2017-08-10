using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;

namespace CasinoModel.Migrations
{
public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
{
  protected override void Generate(MigrationOperation migrationOperation)
  {
    var operation = migrationOperation as CreateViewOperation;

    if (operation != null)
    {
      using (IndentedTextWriter writer = Writer())
      {
        writer.WriteLine("CREATE VIEW {0} AS {1} ; ",
                          operation.ViewName,
                          operation.ViewString);
        Statement(writer);
      }
    }
  }
}
}