using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class NashGamingContext : ApplicationDbContext
    {
        public virtual DbSet<Gamer> Gamers { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MainTeam> Teams { get; set; }
        public virtual DbSet<League> Leagues { get; set; }

        public virtual DbSet<TeamInvite> Invites { get; set; }
        public virtual DbSet<Ladder> Ladders { get; set; }
        public virtual DbSet<Challenge> Challenges { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ladder>().HasMany(o => o.MainTeams).WithMany(x => x.Ladders).Map(ox => { ox.MapLeftKey("MainTeamID"); ox.MapRightKey("LadderID"); ox.ToTable("LadderMainTeams"); });
            modelBuilder.Entity<League>().HasMany(o => o.MainTeams).WithMany(x => x.Leagues).Map(ox => { ox.MapLeftKey("MainTeamID"); ox.MapRightKey("LeagueID"); ox.ToTable("LeagueMainTeams"); });
            base.OnModelCreating(modelBuilder);
        }
    }
}