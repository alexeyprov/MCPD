namespace SimplisticEF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtistRowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("music.Artist", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("music.Artist", "RowVersion");
        }
    }
}
