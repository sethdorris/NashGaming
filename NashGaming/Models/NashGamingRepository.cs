using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
       
        public List<Team> GetAllTeams()
        {
            var query = from teams in context.Teams select teams;
            return query.ToList();
        }

        public List<Match> GetAllMatches()
        {
            var query = from matches in context.Matches select matches;
            return query.ToList();
        }
    }
}