using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace CasinoModel.Migrations
{
  public static class Extensions
  {
    public static void CreateView(this DbMigration migration, string viewName, string viewqueryString)
    {
      ((IDbMigration) migration)
        .AddOperation(new CreateViewOperation(viewName, viewqueryString));
    }
  }
}