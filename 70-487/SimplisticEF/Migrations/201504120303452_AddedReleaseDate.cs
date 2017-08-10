namespace SimplisticEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReleaseDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("music.Album", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("music.Album", "ReleaseDate");
        }
    }
}
