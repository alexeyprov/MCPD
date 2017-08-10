using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using DomainClasses;
//using System.Data.Entity.Config;

//using DateTimePropertyConfiguration = System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.DateTimePropertyConfiguration;

namespace MSDNEF6Article.DataLayer
{
  public class CasinoSlotsModel : DbContext
  {
    public CasinoSlotsModel()
    {
    }

    public CasinoSlotsModel(string connectionStringName) : base(connectionStringName)
    {
    }

    public DbSet<Casino> Casinos { get; set; }
    public DbSet<SlotMachine> SlotMachines { get; set; }
    public DbSet<PokerTable> PokerTables { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<FrenchJournal> Journaux { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);


      modelBuilder.Entity<PokerTable>().Property(c => c.SerialNo).HasColumnType("nvarchar");
      modelBuilder.Properties().Where(p => p.PropertyType == typeof (String))
        .Configure(p => p.HasColumnType("nvarchar"));
      modelBuilder.HasDefaultSchema("Casino");
      //CustomConventions.SetStringTypeConvention(modelBuilder);
      //CustomConventions.SetStringLengthConvention(modelBuilder);
      // modelBuilder.Conventions.Add(new DateTimeColumnTypeConvention());
      base.OnModelCreating(modelBuilder);
    }
  }

  public class MyInitializer : DropCreateDatabaseAlways<CasinoSlotsModel>
  {
    protected override void Seed(CasinoSlotsModel context)
    {
      var casino = new Casino {Description = "Bets are on!", Name = "Gamble It Away", Rating = CasinoRating.Nice};
      casino.SlotMachines.Add(new SlotMachine
                                {
                                  DateInService = DateTime.Now,
                                  SerialNumber = "123456",
                                  SlotMachineType = SlotMachineType.BuyAPay
                                });
      context.Casinos.Add(casino);
      var casino2 = new Casino {Description = "We are shiny!", Name = "Harrahs", Rating = CasinoRating.JustLikeonTv};
      casino2.SlotMachines.Add(new SlotMachine
                                 {
                                   DateInService = DateTime.Now,
                                   SerialNumber = "654321",
                                   SlotMachineType = SlotMachineType.BuyAPay
                                 });
      casino2.SlotMachines.Add(new SlotMachine
                                 {
                                   DateInService = DateTime.Now,
                                   SerialNumber = "789111",
                                   SlotMachineType = SlotMachineType.BuyAPay
                                 });

      context.Casinos.Add(casino2);
      base.Seed(context);
    }
  }


  public class MyHistoryContext : HistoryContext
  {
    public MyHistoryContext(DbConnection dbConnection, string defaultSchema)
      : base(dbConnection, defaultSchema)
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<HistoryRow>().ToTable("__MigrationHistory", "admin");

      //modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(250);
      //modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100);
    }
  }
}