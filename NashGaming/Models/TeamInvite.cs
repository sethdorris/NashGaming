using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class TeamInvite
    {
        [Key]
        public int TeamInviteID { get; set; }
        public virtual Team Team { get; set; }
        public virtual Gamer InvitedGamer { get; set; }
        public DateTime DateSent { get; set; }
        public bool Accepted { get; set; }
    }
}
