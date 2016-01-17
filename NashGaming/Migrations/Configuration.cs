namespace NashGaming.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NashGaming.Models.NashGamingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NashGaming.Models.NashGamingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            List<League> seedLeague = new List<League>
            {
                 new League { LeagueName = "COD" },
                 new League { LeagueName = "Halo" },
                 new League { LeagueName = "BF4" }
            };
            List<Gamer> seedGamer = new List<Gamer>
            {
                new Gamer { Handle = "Blah" }
            };
            context.Leagues.AddRange(seedLeague);
            context.Gamers.AddRange(seedGamer);
        }
    }
}
