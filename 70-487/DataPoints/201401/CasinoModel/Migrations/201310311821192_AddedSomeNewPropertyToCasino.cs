
namespace CasinoModel.Migrations
{
 using System.Data.Entity.Migrations;
    
    public partial class AddedSomeNewPropertyToCasino : DbMigration
    {
        public override void Up()
        {
            AddColumn("Casino.Casinos", "AgainSomeNewProperty", c => c.String(maxLength: 4000));
            this.CreateView("dbo.CasinosWithOver100SlotMachines",
                  "SELECT  * " +
                  "FROM    Casino.Casinos " +
                  "WHERE  Id IN  (SELECT   CasinoId AS Id " +
                  "  FROM     Casino.SlotMachines " +
                  "  GROUP BY CasinoId " +
                  "  HAVING COUNT(CasinoId)>=100) ");



        }
        
        public override void Down()
        {
            DropColumn("Casino.Casinos", "AgainSomeNewProperty");
        }
    }
}
