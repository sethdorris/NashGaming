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
    public class SubTeam : IComparable
    {
        [Key, ForeignKey("MainTeam")]
        public int SubTeamID { get; set; }
        public string SubTeamName { get; set; }
        [Required]
        public MainTeam MainTeam { get; set; }
        public virtual Gamer Captain { get; set; }
        public virtual League League { get; set; }
        public virtual Ladder Ladder { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public virtual List<Gamer> Roster { get; set; }
        public bool Active { get; set; }

        public int CompareTo(object obj)
        {
            SubTeam subteam = obj as SubTeam;
            return this.Points.CompareTo(subteam.Points);
        }
        public override bool Equals(object obj)
        {
            SubTeam a = obj as SubTeam;
            if (obj == null)
            {
                return false;
            }
            return a.SubTeamID == this.SubTeamID;
        }
    }
    public class SubTeamConfiguration : EntityTypeConfiguration<SubTeam>
    {
        public SubTeamConfiguration()
        {
            HasRequired(o => o.MainTeam)
                .WithRequiredDependent();
            HasRequired(o => o.Captain)
                .WithRequiredDependent();
        }
    }
}
