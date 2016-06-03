﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NashGaming.Models
{
    public class Gamer : IdentityUser, IComparable
    {
        [Key]
        public int GamerID { get; set; }
        public virtual string RealUserID { get; set; }
        public string XB1Gamertag { get; set; }
        public string PSNID { get; set; }
        [ForeignKey("TeamID")]
        public virtual MainTeam MainTeam { get; set; }
        public virtual List<Posts> Comments { get; set; }
        public virtual List<TeamInvite> TeamInvites { get; set; }
		public string DisplayName { get; set; }
		public bool Active { get; set; }

        public int CompareTo(object obj)
        {
            Gamer gamer = obj as Gamer;
            return this.GamerID.CompareTo(gamer.GamerID);
        }
        public override bool Equals(object obj)
        {
            Gamer a = obj as Gamer;
            if (obj == null)
            {
                return false;
            }
            return a.GamerID == this.GamerID;
        }
    }
    public class GamerConfiguration : EntityTypeConfiguration<Gamer>
    {
        public GamerConfiguration()
        {
            Property(o => o.Active)
                .IsOptional();
            this.HasOptional(o => o.Comments);
            this.HasOptional(o => o.TeamInvites);
        }
    }
}