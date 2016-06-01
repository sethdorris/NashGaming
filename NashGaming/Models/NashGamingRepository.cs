using System;
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
            try
            {
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

        public List<League> GetLeaguesByStartDate(DateTime start)
        {
            var query = from leagues in context.Leagues.Where(o => o.StartDate == start) orderby leagues.StartDate ascending select leagues;
            return query.ToList();
        }

        public List<League> GetLeaguesByEndDate(DateTime end)
        {
            var query = from leagues in context.Leagues.Where(o => o.EndDate == end) orderby leagues.EndDate ascending select leagues;
            return query.ToList();
        }

        public List<League> GetLeaguesByMinPlayers(int playercount)
        {
            var query = from leagues in context.Leagues.Where(o => o.MinPlayers == playercount) orderby leagues.StartDate ascending select leagues;
            return query.ToList();
        }
        public List<League> GetLeaguesByMaxPlayers(int playercount)
        {
            var query = from leagues in context.Leagues.Where(o => o.MaxPlayers == playercount) orderby leagues.StartDate ascending select leagues;
            return query.ToList();
        }

        public List<Challenge> GetChallengesByLadderID(int ladderid)
        {
            var query = from challenges in context.Challenges.Where(o => o.Ladder.LadderID == ladderid) orderby challenges.Ladder select challenges;
            return query.ToList();
        }

        public List<Challenge> GetChallengesByRecipientTeamID(int teamid)
        {
            var query = from recipient in context.Challenges.Where(o => o.Recipient.MainTeam.TeamID == teamid) orderby recipient.Recipient select recipient;
            return query.ToList();
        }

        public List<Challenge> GetChallengesByInitiatorTeamID(int teamid)
        {
            var query = from initiator in context.Challenges.Where(o => o.Initiator.MainTeam.TeamID == teamid) orderby initiator.Initiator select initiator;
            return query.ToList();
        }

        public List<Ladder> GetLaddersByGameTitle(string title)
        {
            var query = from ladders in context.Ladders.Where(o => Regex.IsMatch(o.GameTitle, title, RegexOptions.IgnoreCase)) orderby ladders.GameTitle select ladders;
            return query.ToList();
        }

        public Gamer GetGamerById(int id)
        {
            var query = from gamers in context.Gamers.Where(o => o.GamerID == id) select gamers;
            return query.Single();
        }

        public bool DeleteGamerById(int id)
        {
            var query = from gamers in context.Gamers.Where(o => o.GamerID == id) select gamers;
            Gamer g = query.Single();
            g.Active = false;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGamerMainTeam(int gamerID, MainTeam mt)
        {
            Gamer g = this.GetGamerById(gamerID);
            g.MainTeam = mt;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGamerComments(int gid, Posts post)
        {
            Gamer g = this.GetGamerById(gid);
            g.Comments.Add(post);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGamerTeamInvites(int gid, TeamInvite inv)
        {
            Gamer g = this.GetGamerById(gid);
            g.TeamInvites.Add(inv);
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Posts GetPostByID(int id)
        {
            var query = from posts in context.Posts.Where(o => o.PostID == id) select posts;
            return query.Single();
        }

        public bool EditPostContent(int id, string content)
        {
            Posts p = GetPostByID(id);
            p.Content = content;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddLeague(League l)
        {
            try
            {
                context.Leagues.Add(l);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddMatch(NashGaming.Models.Match match)
        {
            try
            {
                context.Matches.Add(match);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public League GetLeagueByID(int id)
        {
            var query = from leagues in context.Leagues.Where(o => o.LeagueID == id) select leagues;
            return query.Single();
        }

        public bool AddLeagueTeams(int leagueid, SubTeam t)
        {
            League q = GetLeagueByID(leagueid);
            try
            {
                q.Teams.Add(t);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public Match GetMatchById(int id)
        {
            var query = from matches in context.Matches.Where(o => o.MatchID == id) select matches;
            return query.Single();
        }

        public bool UpdateMatchDatePlayed(int matchID, DateTime date)
        {
            Match match = this.GetMatchById(matchID);
            match.DatePlayed = date;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveLeagueTeams(int leagueid, SubTeam t)
        {
            League l = GetLeagueByID(leagueid);
            try
            {
                l.Teams.Remove(t);
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }


        public bool UpdateMatchResult(int matchID, string result)
        {
            Match match = this.GetMatchById(matchID);
            match.Result = result;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InactivateLeague(int lid)
        {
            League q = GetLeagueByID(lid);
            try
            {
                q.Active = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Ladder> GetAllLadders()
        {
            var query = from ladders in context.Ladders select ladders;
            return query.ToList();
        }

        public bool AddLadder(Ladder l)
        {
            try
            {
                context.Ladders.Add(l);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateLeagueMinPlayers(int lid, int minplayers)
        {
            League l = this.GetLeagueByID(lid);
            try
            {
                l.MinPlayers = minplayers;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateLeagueMaxPlayers(int lid, int max)
        {
            League l = this.GetLeagueByID(lid);
            try
            {
                l.MaxPlayers = max;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateLeagueName(int id, string name)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.LeagueName = name;
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool UpdateMatchTeam1Score(int matchID, int score1)
        {
            Match match = this.GetMatchById(matchID);
            match.Team1Score = score1;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateLeagueGamesPerWeek(int id, int games)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.GamesPerWeek = games;
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool UpdateMatchTeam2Score(int matchID, int score2)
        {
            Match match = this.GetMatchById(matchID);
            match.Team2Score = score2;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddMatchToLeague(int id, NashGaming.Models.Match m)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.Matches.Add(m);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveMatchFromLeague(int id, NashGaming.Models.Match m)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.Matches.Remove(m);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPostToLeague(int id, Posts p)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.Feed.Add(p);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemovePostFromLeague(int id, Posts p)
        {
            League l = this.GetLeagueByID(id);
            try
            {
                l.Feed.Remove(p);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Ladder GetLadderById(int id)
        {
            var query = from ladders in context.Ladders.Where(o => o.LadderID == id) select ladders;
            return query.Single();
        }

        public bool AddPostToLadder(int id, Posts p)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Feed.Add(p);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemovePostFromLadder(int id, Posts p)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Feed.Remove(p);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveTeamFromLadder(int id, SubTeam t)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Teams.Remove(t);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddTeamToLadder(int id, SubTeam t)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Teams.Add(t);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddMatchToLadder(int id, NashGaming.Models.Match m)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Matches.Add(m);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveMatchFromLadder(int id, NashGaming.Models.Match m)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Matches.Remove(m);
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool UpdateMatchCompletion(int matchID, bool complete)
        {
            Match match = this.GetMatchById(matchID);
            match.Completed = complete;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveChallengeFromLadder(int id, Challenge c)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Challenges.Remove(c);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddChallengeFromLadder(int id, Challenge c)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Challenges.Add(c);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InactivateLadder(int id)
        {
            Ladder l = this.GetLadderById(id);
            try
            {
                l.Active = false;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddNewMainTeam(MainTeam t)
        {
            try
            {
                context.Teams.Add(t);
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public MainTeam GetMainTeamByID(int id)
        {
            var query = from Teams in context.Teams.Where(o => o.TeamID == id) select Teams;
            return query.Single();
        }
        public bool InactivateMainTeam(int id)
        {
            MainTeam team = this.GetMainTeamByID(id);
            try
            {
                team.Active = false;
                context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}