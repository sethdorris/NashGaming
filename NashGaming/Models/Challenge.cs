using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class Challenge
    {
        [Key]
        public int ChallengeID { get; set; }
        public SubTeam Initiator { get; set; }
        public SubTeam Recipient { get; set; }
        public DateTime ProposedDate1 { get; set; }
        public DateTime ProposedDate2 { get; set; }
        public DateTime ProposedDate3 { get; set; }
        public bool Accepted { get; set; }
        public Ladder Ladder { get; set; }
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
