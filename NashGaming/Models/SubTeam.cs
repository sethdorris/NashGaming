using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class SubTeam : IComparable
    {
        public int SubTeamID { get; set; }
        public string SubTeamName { get; set; }
        public MainTeam MainTeam { get; set; }
        public virtual Gamer Captain { get; set; }
        public virtual League League { get; set; }
        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public virtual List<Gamer> Roster { get; set; }

        public int CompareTo(object obj)
        {
            SubTeam subteam = obj as SubTeam;
            return this.SubTeamName.CompareTo(subteam.SubTeamName);
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
}
