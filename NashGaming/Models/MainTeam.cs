﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class MainTeam : IComparable
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public DateTime DateFounded { get; set; }
        [Required]
        [ForeignKey("GamerID")]
        public Gamer Founder { get; set; }
        public int FounderID { get; set; }
        public string Website { get; set; }
        public bool Active { get; set; }
        [ForeignKey("SubTeamID")]
        public List<SubTeam> SubTeams { get; set; }
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
            return a.TeamID == this.TeamID;
        }
    }
    public class MainTeamConfiguration : EntityTypeConfiguration<MainTeam>
    {
        public MainTeamConfiguration()
        {
            HasOptional(o => o.SubTeams);
        }
    }
}