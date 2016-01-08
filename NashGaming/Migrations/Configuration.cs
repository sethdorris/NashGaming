namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NashGaming.Models;
    using System.Collections.Generic;

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
            context.Leagues.AddRange(
                new List<League> { new League { LeagueName = "Siege", Platform = "PS4"},
                                   new League { LeagueName = "COD", Platform = "PS4"},
                                   new League { LeagueName = "COD", Platform = "XB1" }
            });
           

            context.Teams.AddRange(
                new List<Team> { new Team { Rank = 1, Active = true, TeamName = "Siege"},
                                 new Team { Rank = 2, Active = true, TeamName = "Seed2"}
            });

            context.Gamers.AddRange(
                new List<Gamer> { new Gamer { Handle = "StiffNasty" },
                                  new Gamer { Handle = "Pizzle" }
            });
            

        }
    }
}
