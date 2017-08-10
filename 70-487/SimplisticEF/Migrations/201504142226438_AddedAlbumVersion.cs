namespace SimplisticEF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedAlbumVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("music.Album", "Version", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("music.Album", "Version");
        }
    }
}
