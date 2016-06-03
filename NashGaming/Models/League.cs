using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class League
    {
        [Key]
        public int LeagueID { get; set; }
        public virtual List<SubTeam> Teams { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string LeagueName { get; set; }
        public int GamesPerWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SeasonLength { get; set; }
        public string LeagueType { get; set; }
        public string GameTitle { get; set; }
        public virtual List<Match> Matches { get; set; }
        [MaxLength(3)]
        [MinLength(2)]
        public string Platform { get; set; }
        public virtual List<Posts> Feed { get; set; }
        public bool Active { get; set; }
        public override bool Equals(object obj)
        {
            League o = obj as League;
            if (o == null)
            {
                return false;
            }
            return o.LeagueID == this.LeagueID;
        }
    }
    public class LeagueConfiguration : EntityTypeConfiguration<League>
    {
        public LeagueConfiguration()
        {
            HasOptional(o => o.Matches);
            HasOptional(o => o.Teams);
            HasOptional(o => o.Feed);
        }
    }
}