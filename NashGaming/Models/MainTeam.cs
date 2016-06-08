using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace NashGaming.Models
{
    public class MainTeam : IComparable
    {
        public int MainTeamID { get; set; }
        public string TeamName { get; set; }
        public DateTime DateFounded { get; set; }
        public string Website { get; set; }
        public bool Active { get; set; }
        public ICollection<Ladder> Ladders { get; set; }
        public ICollection<League> Leagues { get; set; }
        public ICollection<Gamer> Gamers { get; set; }
        //public ICollection<Challenge> Challenges { get; set; }
		public string LogoLink { get; set; }

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
            return a.MainTeamID == this.MainTeamID;
        }
    }
}