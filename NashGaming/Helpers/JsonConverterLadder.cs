using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Helpers
{
    public class JsonConverterLadder
    {
        public int LadderID { get; set; }
        public string LadderName { get; set; }
        public string GameTitle { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string Platform { get; set; }
        public bool Active { get; set; }
        public List<Ladder> LadderList { get; set; }
        public Ladder Ladder { get; set; }
        public JsonConverterLadder()
        {

        }
        public JsonConverterLadder(List<Ladder> list)
        {
            LadderList = list;
        }

        public JsonConverterLadder(Ladder ladder)
        {
            Ladder = ladder;
        }

        public List<JsonConverterLadder> ConvertLadderList()
        {
            List<JsonConverterLadder> result = new List<JsonConverterLadder>();
            foreach (Ladder l in LadderList)
            {
                JsonConverterLadder row = new JsonConverterLadder();
                row.Active = l.Active;
                row.GameTitle = l.GameTitle;
                row.LadderID = l.LadderID;
                row.LadderName = l.LadderName;
                row.MaxPlayers = l.MaxPlayers;
                row.MinPlayers = l.MinPlayers;
                row.Platform = l.Platform;
                result.Add(row);
            }
            return result;
        }
    }
}
