﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NashGaming.Models
{
    public class NashGamingRepository
    {
        private NashGamingContext context;
        public NashGamingContext Context { get { return context; } }

        public NashGamingRepository()
        {
            context = new NashGamingContext();
        }

        public NashGamingRepository(NashGamingContext a_context)
        {
            context = a_context;
        }

        public List<Gamer> GetAllGamers()
        {
            var query = from gamers in context.Gamers select gamers;
            return query.ToList();
        }

        //public List<Gamer> GetGamerByHandle(string handle)
        //{
        //    var query = from gamers in context.Gamers select gamers;
        //    List<Gamer> found_gamers = query.Where(o => Regex.IsMatch(o.RealUserID, handle, RegexOptions.IgnoreCase)).ToList();
        //    found_gamers.Sort();
        //    return found_gamers;
        //}
       
        public List<MainTeam> GetAllMainTeams()
        {
            var query = from teams in context.Teams select teams;
            return query.ToList();
        }

        public List<Match> GetAllMatches()
        {
            var query = from matches in context.Matches select matches;
            return query.ToList();
        }

        public List<MainTeam> SearchTeamsByName(string teamname)
        {
            var query = from team in context.Teams select team;
            List<MainTeam> found_teams = query.Where(t => Regex.IsMatch(t.TeamName, teamname, RegexOptions.IgnoreCase)).ToList();
            found_teams.Sort();
            return found_teams;
        }

        public List<Posts> GetAllPosts()
        {
            var query = from post in context.Posts orderby post.Date descending select post;
            List<Posts> allposts = query.ToList();
            return allposts;
        }

        public List<Posts> SearchPostsByContent(string search)
        {
            var query = from post in context.Posts.Where(o => Regex.IsMatch(o.Content, search, RegexOptions.IgnoreCase)) orderby post.Date descending select post;
            List<Posts> matchingposts = query.ToList();
            return matchingposts;
        }

        public bool DeletePostById(int id)
        {
            var query = from post in context.Posts.Where(o => o.PostID == id) select post;
            Posts result = query.Single();
            try {
                context.Posts.Remove(result);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }   
        }

        public bool CreateAPost(Gamer gamer, string content)
        {
            Posts new_post = new Posts { Author = gamer, Content = content, Date = DateTime.Now };
            try
            {
                context.Posts.Add(new_post);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TeamInvite> GetTeamInvitesByGamerID(int GID)
        {
            var query = from invites in context.Invites.Where(o => o.InvitedGamer.GamerID == GID) select invites;
            return query.ToList();
        }

        public List<League> GetLeaguesByGamesPerWeek(int GamesPerWeek)
        {
            var query = from leagues in context.Leagues.Where(o => o.GamesPerWeek == GamesPerWeek) orderby leagues.GameTitle select leagues;
            return query.ToList();
        }

        public List<League> GetLeaguesBySeasonLength(int SeasonLength)
        {
            var query = from leagues in context.Leagues.Where(o => o.SeasonLength == SeasonLength) orderby leagues.GameTitle select leagues;
            return query.ToList();
        }

        public List<Match> GetMatchesByTeamID(int MainTeamID)
        {
            var query = from matches in context.Matches.Where(o => o.Team1.MainTeam.TeamID == MainTeamID || o.Team2.MainTeam.TeamID == MainTeamID) orderby matches.League.GameTitle select matches;
            return query.ToList();
        }

        public bool AddGamer(Gamer gamer)
        {
            try
            {
                context.Gamers.Add(gamer);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<League> GetAllLeagues()
        {
            var query = from leagues in context.Leagues select leagues;
            return query.ToList();
        }

        public MainTeam getTeamById(int teamID)
        {
            var query = from teams in context.Teams.Where(o => o.TeamID == teamID) select teams;
            return query.FirstOrDefault();
        }

        public List<SubTeam> getTeamsByLeagueId(int LeagueID)
        {
            var query = from teams in context.Leagues.Where(o => o.LeagueID == LeagueID) select teams.Teams;
            return query.FirstOrDefault();
        }

        public List<TeamInvite> getInvitesByTeamID(int TeamID)
        {
            var query = from invites in context.Invites.Where(o => o.Team.TeamID == TeamID) select invites;
            return query.ToList();
        }

        public List<TeamInvite> GetAllTeamInvites()
        {
            var query = from invites in context.Invites select invites;
            return query.ToList();
        }

        public bool DeleteTeamInviteByID(int ID)
        {
            var query = from invites in context.Invites.Where(o => o.TeamInviteID == ID) select invites;
            TeamInvite invite = query.Single();
            try
            {
                context.Invites.Remove(invite);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<NashGaming.Models.Match> GetMatchesByTeamName(string teamname)
        {
            var query = from matches in context.Matches.Where(o => Regex.IsMatch(o.Team1.MainTeam.TeamName, teamname, RegexOptions.IgnoreCase) || Regex.IsMatch(o.Team2.MainTeam.TeamName, teamname, RegexOptions.IgnoreCase)) select matches;
            return query.ToList();
        }

        public List<NashGaming.Models.Match> GetMatchesByDatePlayed(DateTime played)
        {
            var query = from matches in context.Matches.Where(o => o.DatePlayed == played) select matches;
            return query.ToList();
        }

        public List<NashGaming.Models.Match> GetMatchesByLeagueID(int LID)
        {
            var query = from matches in context.Matches.Where(o => o.League.LeagueID == LID) select matches;
            return query.ToList();
        }

        public List<NashGaming.Models.Match> GetMatchesByGameTitle(string title)
        {
            var query = from matches in context.Matches.Where(o => Regex.IsMatch(o.League.GameTitle, title, RegexOptions.IgnoreCase)) orderby matches.DatePlayed ascending select matches;
            return query.ToList();
        }
    }
}