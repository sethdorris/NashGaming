using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Helpers
{
    public class JsonConverterMatches
    {
        public int MatchID { get; set; }
        public DateTime DatePlayed { get; set; }
        public virtual string Team1 { get; set; }
        public virtual string Team2 { get; set; }
        public string Result { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public virtual int? LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueTitle { get; set; }
        public string LeaguePlatform { get; set; }
        public virtual int? LadderId { get; set; }
        public string LadderName { get; set; }
        public string LadderTitle { get; set; }
        public string LadderPlatform { get; set; }
        public bool Completed { get; set; }
        public List<Match> MatchList { get; set; }
        public Match Match { get; set; }
        public JsonConverterMatches()
        {

        }
        public JsonConverterMatches(List<Match> m)
        {
            MatchList = m;
        }
        public JsonConverterMatches(Match m)
        {
            Match = m;
        }
        public List<JsonConverterMatches> SerializeMatches()
        {
            List<JsonConverterMatches> result = new List<JsonConverterMatches>();
            foreach (Match x in MatchList)
            {
                JsonConverterMatches row = new JsonConverterMatches();
                if (x.Ladder != null)
                {
                    row.LadderId = x.Ladder.LadderID;
                    row.LadderName = x.Ladder.LadderName;
                    row.LadderTitle = x.Ladder.GameTitle;
                    row.LadderPlatform = x.Ladder.Platform;
                }
                if (x.League != null)
                {
                    row.LeagueId = x.League.LeagueID;
                    row.LeagueName = x.League.LeagueName;
                    row.LeagueTitle = x.League.GameTitle;
                    row.LeaguePlatform = x.League.Platform;
                }
                row.Result = x.Result;
                row.Team1 = x.Team1.TeamName;
                row.Team2 = x.Team2.TeamName;
                row.Team1Score = x.Team1Score;
                row.Team2Score = x.Team2Score;
                row.Completed = x.Completed;
                row.DatePlayed = x.DatePlayed;
                result.Add(row);
            }
            return result;
        }
        public JsonConverterMatches SerializeMatch()
        {
            JsonConverterMatches result = new JsonConverterMatches();
            result.LadderId = Match.Ladder.LadderID;
            result.LeagueId = Match.League.LeagueID;
            result.Result = Match.Result;
            result.Team1 = Match.Team1.TeamName;
            result.Team2 = Match.Team2.TeamName;
            result.Team1Score = Match.Team1Score;
            result.Team2Score = Match.Team2Score;
            result.Completed = Match.Completed;
            result.DatePlayed = Match.DatePlayed;
            return result;
        }
    }
}
