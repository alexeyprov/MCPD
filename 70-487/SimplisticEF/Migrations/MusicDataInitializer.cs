using System.Data.Entity;
using SimplisticEF.Models;

namespace SimplisticEF.Migrations
{
    public class MusicDataInitializer : CreateDatabaseIfNotExists<MusicContext>
    {
        protected override void Seed(MusicContext context)
        {
            base.Seed(context);

            Artist threeDoors = new Artist
                {
                    Name = "3 Doors Down"
                };
            Artist cure = new Artist
                {
                    Name = "The Cure"
                };
            Artist kino = new Artist
                {
                    Name = "Kino"
                };

            context.Artists.AddRange(
                new[]
                {
                    threeDoors,
                    kino,
                    cure
                });

            context.Albums.AddRange(
                new[]
                {
                    new Album
                    {
                        Name = "45",
                        Price = 6M,
                        Artist = kino
                    },
                    new Album
                    {
                        Name = "46",
                        Price = 6.5M,
                        Artist = kino
                    },
                    new Album
                    {
                        Name = "Wish",
                        Price = 9.98M,
                        Artist = cure
                    },
                    new Album
                    {
                        Name = "Away from the Sun",
                        Price = 5M,
                        Artist = threeDoors
                    },
                });
        }
    }
}