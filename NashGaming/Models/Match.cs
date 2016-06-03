using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Match : IComparable
    {
        [Key]
        public int MatchID { get; set; }
        public DateTime DatePlayed { get; set; }
        [Required]
        public virtual SubTeam Team1 { get; set; }
        public int Team1ID { get; set; }
        [Required]
        public virtual SubTeam Team2 { get; set; }
        public int Team2ID { get; set; }
        public string Result { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public virtual League League { get; set; }
        public int LeagueID { get; set; }
        public virtual Ladder Ladder { get; set; }
        public int LadderID { get; set; }
        public bool Completed { get; set; }
        public int CompareTo(object obj)
        {
            Match m = obj as Match;
            return -1*( this.DatePlayed.CompareTo(m.DatePlayed));
        }
        public override bool Equals(object obj)
        {
            Match o = obj as Match;
            if (o == null)
            {
                return false;
            }
            return o.MatchID == this.MatchID;
        }
    }
    public class MatchConfiguration : EntityTypeConfiguration<Match>
    {
        public MatchConfiguration()
        {
            HasOptional(o => o.Ladder);
            HasOptional(o => o.League);
            Property(o => o.DatePlayed)
                .IsOptional();
            HasOptional(o => o.Result);

        }
    }
}