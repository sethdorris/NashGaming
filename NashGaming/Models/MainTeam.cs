using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class MainTeam : IComparable
    {
        [Key, ForeignKey("Founder")]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public DateTime DateFounded { get; set; }
        public Gamer Founder { get; set; }
        public string Website { get; set; }
        public bool Active { get; set; }
        public List<SubTeam> SubTeams { get; set; }

        public int CompareTo(object obj)
        {
            MainTeam team = obj as MainTeam;
            return this.TeamName.CompareTo(team.TeamName);
        }
        public override bool Equals(object obj)
        {
            MainTeam a = obj as MainTeam;
            if (obj == null)
            {
                return false;
            }
            return a.TeamID == this.TeamID;
        }
    }
}