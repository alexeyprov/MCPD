namespace SimplisticEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBaseMusicEntity_TPC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("music.Album", "Artist_ArtistId", "music.Artist");
            DropForeignKey("music.ArtistDetail", "ArtistId", "music.Artist");
            DropForeignKey("music.Album", "Reviewer_ReviewerId", "music.Reviewer");
            DropForeignKey("music.AlbumReviewer", "Reviewer_ReviewerId", "music.Reviewer");
            DropPrimaryKey("music.Artist");
            DropPrimaryKey("music.Reviewer");
            AddColumn("music.Artist", "Country", c => c.String());
            AddColumn("music.Reviewer", "Country", c => c.String());
            AlterColumn("music.Artist", "ArtistId", c => c.Int(nullable: false));
            AlterColumn("music.Reviewer", "ReviewerId", c => c.Int(nullable: false));
            AlterColumn("music.Reviewer", "Name", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("music.Artist", "ArtistId");
            AddPrimaryKey("music.Reviewer", "ReviewerId");
            AddForeignKey("music.Album", "Artist_ArtistId", "music.Artist", "ArtistId");
            AddForeignKey("music.ArtistDetail", "ArtistId", "music.Artist", "ArtistId");
            AddForeignKey("music.Album", "Reviewer_ReviewerId", "music.Reviewer", "ReviewerId");
            AddForeignKey("music.AlbumReviewer", "Reviewer_ReviewerId", "music.Reviewer", "ReviewerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("music.AlbumReviewer", "Reviewer_ReviewerId", "music.Reviewer");
            DropForeignKey("music.Album", "Reviewer_ReviewerId", "music.Reviewer");
            DropForeignKey("music.ArtistDetail", "ArtistId", "music.Artist");
            DropForeignKey("music.Album", "Artist_ArtistId", "music.Artist");
            DropPrimaryKey("music.Reviewer");
            DropPrimaryKey("music.Artist");
            AlterColumn("music.Reviewer", "Name", c => c.String());
            AlterColumn("music.Reviewer", "ReviewerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("music.Artist", "ArtistId", c => c.Int(nullable: false, identity: true));
            DropColumn("music.Reviewer", "Country");
            DropColumn("music.Artist", "Country");
            AddPrimaryKey("music.Reviewer", "ReviewerId");
            AddPrimaryKey("music.Artist", "ArtistId");
            AddForeignKey("music.AlbumReviewer", "Reviewer_ReviewerId", "music.Reviewer", "ReviewerId", cascadeDelete: true);
            AddForeignKey("music.Album", "Reviewer_ReviewerId", "music.Reviewer", "ReviewerId");
            AddForeignKey("music.ArtistDetail", "ArtistId", "music.Artist", "ArtistId");
            AddForeignKey("music.Album", "Artist_ArtistId", "music.Artist", "ArtistId");
        }
    }
}
