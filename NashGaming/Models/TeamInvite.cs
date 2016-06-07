using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class TeamInvite
    {
        public int TeamInviteID { get; set; }
        [Required]
        public virtual MainTeam Team { get; set; }
        public virtual Gamer InvitedGamer { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateAccepted { get; set; }
        public bool Accepted { get; set; }
        public override bool Equals(object obj)
        {
            TeamInvite i = obj as TeamInvite;
            if (obj == null)
            {
                return false;
            }
            return i.TeamInviteID == this.TeamInviteID;
        }
    }
    public class TeamInviteConfiguration : EntityTypeConfiguration<TeamInvite> {
        public TeamInviteConfiguration()
        {

        }
    }
}
