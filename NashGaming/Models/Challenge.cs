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
        [Key]
        public int ChallengeID { get; set; }
        [Required]
        public virtual SubTeam Initiator { get; set; }
        [Required]
        [ForeignKey("Initiator")]
        public int InitiatorID { get; set; }
        [Required]
        public virtual SubTeam Recipient { get; set; }
        [Required]
        [ForeignKey("Recipient")]
        public int RecipientID { get; set; }
        [Required]
        public DateTime ProposedDate1 { get; set; }
        [Required]
        public DateTime ProposedDate2 { get; set; }
        [Required]
        public DateTime ProposedDate3 { get; set; }
        public bool Accepted { get; set; }
        public virtual Ladder Ladder { get; set; }
        [Required]
        public int LadderID { get; set; }
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
    public class ChallengeConfiguration : EntityTypeConfiguration<Challenge>
    {
        public ChallengeConfiguration()
        {
            HasRequired(o => o.Initiator);
            HasRequired(o => o.Recipient);
            HasRequired(o => o.Ladder)
                .WithRequiredDependent();
               
        }
    }
}
