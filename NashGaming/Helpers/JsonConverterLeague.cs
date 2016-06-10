using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Helpers
{
    public class JsonConverterLeague
    {
        public int LeagueID { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string LeagueName { get; set; }
        public int GamesPerWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SeasonLength { get; set; }
        public string LeagueType { get; set; }
        public string GameTitle { get; set; }
        public string Platform { get; set; }
        public bool Active { get; set; }
        public List<League> Leagues { get; set; }
        public League League { get; set; }
        public JsonConverterLeague()
        {

        }
        public JsonConverterLeague(List<League> l)
        {
            Leagues = l;
        }
        public JsonConverterLeague(League l)
        {
            League = l;
        }

        public List<JsonConverterLeague> SerializeLeagues()
        {
            List<JsonConverterLeague> result = new List<JsonConverterLeague>();
            foreach(League l in Leagues)
            {
                JsonConverterLeague row = new JsonConverterLeague();
                row.GamesPerWeek = l.GamesPerWeek;
                row.Active = l.Active;
                row.EndDate = l.EndDate;
                row.GameTitle = l.GameTitle;
                row.LeagueName = l.LeagueName;
                row.LeagueType = l.LeagueType;
                row.MinPlayers = l.MinPlayers;
                row.MaxPlayers = l.MaxPlayers;
                row.Platform = l.Platform;
                row.StartDate = l.StartDate;
                row.SeasonLength = l.SeasonLength;
                result.Add(row);
            }
            return result;
        }
        public JsonConverterLeague SerializeLeague()
        {
            JsonConverterLeague row = new JsonConverterLeague();
            row.GamesPerWeek = League.GamesPerWeek;
            row.Active = League.Active;
            row.EndDate = League.EndDate;
            row.GameTitle = League.GameTitle;
            row.LeagueName = League.LeagueName;
            row.LeagueType = League.LeagueType;
            row.MinPlayers = League.MinPlayers;
            row.MaxPlayers = League.MaxPlayers;
            row.Platform = League.Platform;
            row.StartDate = League.StartDate;
            row.SeasonLength = League.SeasonLength;
            return row;
        }
    }
}
