namespace SimplisticEF.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics;

    public class MusicContext : DbContext
    {
        // Your context has been configured to use a 'MusicContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SimplisticEF.Models.MusicContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MusicContext' 
        // connection string in the application configuration file.
        public MusicContext()
            : base("name=MusicContext")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Album> Albums
        {
            get;
            set;
        }

        public virtual DbSet<Artist> Artists
        {
            get;
            set;
        }

        public virtual DbSet<Reviewer> Reviewers
        {
            get;
            set;
        }

        // TODO: ensure Artist and Reviewer have non-overlapping
        // id's before uncommenting
        // e.g. by removing identity from id columns via:
        // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
        //public virtual DbSet<BaseMusicEntity> Entities
        //{
        //    get;
        //    set;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("music");

            modelBuilder.Entity<Album>()
                .HasMany<Reviewer>(a => a.Reviewers)
                .WithMany()
                .Map(c => c.ToTable("AlbumReviewer", "music"));

            modelBuilder.Entity<Artist>()
                .Map(c =>
                    {
                        c.MapInheritedProperties();
                        c.ToTable("Artist");
                    });

            modelBuilder.Entity<Reviewer>()
                .Map(c => 
                    {
                        c.MapInheritedProperties();
                        c.ToTable("Reviewer");
                    });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}