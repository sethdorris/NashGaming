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
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
    }
}