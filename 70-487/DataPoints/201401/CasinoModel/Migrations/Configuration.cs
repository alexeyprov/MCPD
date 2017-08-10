using DomainClasses;

namespace CasinoModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
  using MSDNEF6Article.DataLayer;

    internal sealed class Configuration : DbMigrationsConfiguration<MSDNEF6Article.DataLayer.CasinoSlotsModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CasinoSlotsModel context)
        {

          var casino = new Casino { Description = "Bets are on!", Name = "Gamble It Away", Rating = CasinoRating.Nice, OpeningDate = new DateTime(2013, 1, 1) };

          casino.SlotMachines.Add(new SlotMachine { DateInService = DateTime.Now, SerialNumber = "123456", SlotMachineType = SlotMachineType.BuyAPay, HasQuietMode = true, LastMaintenance = DateTime.Now });
          casino.MailingAddress = new Casino.Address("1 Main", "maincity", "mc", "01111");
          casino.PhysicalAddress = new Casino.Address("1 Main", "maincity", "mc", "01111");
          var casino2 = new Casino { Description = "We are shiny!", Name = "Harrahs", Rating = CasinoRating.JustLikeonTv, OpeningDate = new DateTime(2013, 1, 1) };
          casino2.MailingAddress = new Casino.Address("1 Main", "Las Vegas", "NV", "01111");
          casino2.PhysicalAddress = new Casino.Address("1 Main", "Las Vegas", "NV", "01111");
          casino2.SlotMachines.Add(new SlotMachine { DateInService = DateTime.Now, SerialNumber = "654321", SlotMachineType = SlotMachineType.BuyAPay,HasQuietMode=true,LastMaintenance=DateTime.Now });
          casino2.SlotMachines.Add(new SlotMachine { DateInService = DateTime.Now, SerialNumber = "789111", SlotMachineType = SlotMachineType.BuyAPay, HasQuietMode = true, LastMaintenance = DateTime.Now });
          context.Casinos.AddOrUpdate(c => c.Name, casino,casino2);
        }
    }
}
