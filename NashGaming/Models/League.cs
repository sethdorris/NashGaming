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
        public int LeagueID { get; set; }
        public virtual ICollection<MainTeam> MainTeams { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string LeagueName { get; set; }
        public int GamesPerWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SeasonLength { get; set; }
        public string LeagueType { get; set; }
        public string GameTitle { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        [MaxLength(3)]
        [MinLength(2)]
        public string Platform { get; set; }
        public virtual ICollection<Posts> Feed { get; set; }
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
}