namespace CasinoModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Casino.Casinos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Rating = c.Int(nullable: false),
                        Description = c.String(maxLength: 4000),
                        PhysicalAddress_StreetOrPoBox = c.String(maxLength: 4000),
                        PhysicalAddress_City = c.String(maxLength: 4000),
                        PhysicalAddress_State = c.String(maxLength: 4000),
                        PhysicalAddress_PostalCode = c.String(maxLength: 4000),
                        MailingAddress_StreetOrPoBox = c.String(maxLength: 4000),
                        MailingAddress_City = c.String(maxLength: 4000),
                        MailingAddress_State = c.String(maxLength: 4000),
                        MailingAddress_PostalCode = c.String(maxLength: 4000),
                        OpeningDate = c.DateTime(nullable: false),
                        SomeNewProperty = c.String(maxLength: 4000),
                        SomeOtherNewProperty = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Casino.SlotMachines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotMachineType = c.Int(nullable: false),
                        SerialNumber = c.String(maxLength: 4000),
                        CasinoId = c.Int(nullable: false),
                        DateInService = c.DateTime(nullable: false),
                        HasQuietMode = c.Boolean(nullable: false),
                        LastMaintenance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Casino.Casinos", t => t.CasinoId, cascadeDelete: true)
                .Index(t => t.CasinoId);
            
            CreateTable(
                "Casino.FrenchJournals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Casino.PokerTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 4000),
                        SerialNo = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Casino.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Casino.SlotMachines", "CasinoId", "Casino.Casinos");
            DropIndex("Casino.SlotMachines", new[] { "CasinoId" });
            DropTable("Casino.Status");
            DropTable("Casino.PokerTables");
            DropTable("Casino.FrenchJournals");
            DropTable("Casino.SlotMachines");
            DropTable("Casino.Casinos");
        }
    }
}
