using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Team : IComparable
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public List<Gamer> Roster { get; set; }
        public DateTime DateFounded { get; set; }
        public virtual Gamer Founder { get; set; }
        public string Website { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Rank { get; set; }
        public List<League> League { get; set; }
        public bool Active { get; set; }

        public int CompareTo(object obj)
        {
            Team team = obj as Team;
            return this.TeamName.CompareTo(team.TeamName);
        }
    }
}