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
    public class Challenge
    {
        public virtual int ChallengeID { get; set; }
        [ForeignKey("InitiatorId")]
        public virtual MainTeam Initiator { get; set; }
        public virtual int InitiatorId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual MainTeam Recipient { get; set; }
        public virtual int RecipientId { get; set; }
        public DateTime ProposedDate1 { get; set; }
        public DateTime ProposedDate2 { get; set; }
        public DateTime ProposedDate3 { get; set; }
        public bool Accepted { get; set; }
        public virtual Ladder Ladder { get; set; }
        public override bool Equals(object obj)
        {
            Challenge c = obj as Challenge;
            if (obj == null)
            {
                return false;
            }
            return c.ChallengeID == this.ChallengeID;
        }
    }
}
