using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class League
    {
        [Key]
        public int LeagueID { get; set; }
        public virtual List<SubTeam> Teams { get; set; }
        public string LeagueName { get; set; }
        public string GameTitle { get; set; }
        public virtual List<Match> Matches { get; set; }
        [MaxLength(3)]
        [MinLength(2)]
        public string Platform { get; set; }
        public virtual List<Posts> Feed { get; set; }
    }
}