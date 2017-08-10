namespace CasinoModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pluralizationservice : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Casino.FrenchJournals", newName: "FrenchJournaux");
            RenameTable(name: "Casino.Status", newName: "Statii");
        }
        
        public override void Down()
        {
            RenameTable(name: "Casino.Statii", newName: "Status");
            RenameTable(name: "Casino.FrenchJournaux", newName: "FrenchJournals");
        }
    }
}
