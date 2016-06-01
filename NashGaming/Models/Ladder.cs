using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class Ladder
    {
        public int LadderID { get; set; }
        public string LadderName { get; set; }
        public string GameTitle { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string Platform { get; set; }
        public virtual List<Posts> Feed { get; set; }
        public virtual List<SubTeam> Teams { get; set; }
        public virtual List<Match> Matches { get; set; }
        public virtual List<Challenge> Challenges { get; set; }
        public bool Active { get; set; }
        public override bool Equals(object obj)
        {
            Ladder a = obj as Ladder;
            if (obj == null)
            {
                return false;
            }
            return a.LadderID == this.LadderID;
        }
    }
}
