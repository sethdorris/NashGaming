using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public virtual SubTeam Team2 { get; set; }
        public string Result { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public virtual League League { get; set; }
        public bool Completed { get; set; }
        public int CompareTo(object obj)
        {
            Match m = obj as Match;
            return -1*( this.DatePlayed.CompareTo(m.DatePlayed));
        }
    }
}