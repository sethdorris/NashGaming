using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class JsonReturnLeagues
    {
        public int LeagueID { get; set; }
        public virtual ICollection<MainTeam> MainTeams { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string LeagueName { get; set; }
        public int GamesPerWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SeasonLength { get; set; }
        public string LeagueType { get; set; }
        public string GameTitle { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public string Platform { get; set; }
        public virtual ICollection<Posts> Feed { get; set; }
        public bool Active { get; set; }
        public List<League> _LeagueList { get; set; }

        public JsonReturnLeagues()
        {

        }

        public JsonReturnLeagues(List<League> leagues)
        {
            _LeagueList = leagues;
        }
        public override bool Equals(object obj)
        {
            League o = obj as League;
            if (o == null)
            {
                return false;
            }
            return o.LeagueID == this.LeagueID;
        }

        public List<JsonReturnLeagues> ReturnLeaguesAsJson()
        {
            List<JsonReturnLeagues> result = new List<JsonReturnLeagues>();
            for (int i = 0; i < _LeagueList.Count; i++)
            {
                JsonReturnLeagues row = new JsonReturnLeagues();
                row.Active = _LeagueList[i].Active;
                row.EndDate = _LeagueList[i].EndDate;
                row.Feed = _LeagueList[i].Feed;
                row.GamesPerWeek = _LeagueList[i].GamesPerWeek;
                row.GameTitle = _LeagueList[i].GameTitle;
                row.LeagueID = _LeagueList[i].LeagueID;
                row.LeagueType = _LeagueList[i].LeagueType;
                row.Matches = _LeagueList[i].Matches;
                row.MaxPlayers = _LeagueList[i].MaxPlayers;
                row.MaxPlayers = _LeagueList[i].MinPlayers;
                row.Platform = _LeagueList[i].Platform;
                row.SeasonLength = _LeagueList[i].SeasonLength;
                row.StartDate = _LeagueList[i].StartDate;
                result.Add(row);
            }
            return result;
        }

    }
}
