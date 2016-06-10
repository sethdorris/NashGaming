using NashGaming.Helpers;
using NashGaming.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NashGaming.Controllers
{
    public class ProfileController : Controller
    {
        private NashGamingRepository _repo;
        private NashGamingContext _context;
        private Gamer currentGamer;
        public ProfileController()
        {
            _repo = new NashGamingRepository();
            _context = new NashGamingContext();
            currentGamer = new Gamer();
        }

        public ActionResult Index()
        {
            Gamer g = _repo.getgamerbyaspusername(User.Identity.Name);
            currentGamer = g;
            return View(g);
        }
        [HttpGet]
        public JsonResult GetInvites()
        {
            List<TeamInvite> result = _repo.GetAllTeamInvites();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AcceptInvite(int id)
        {
            bool UpdatedTeamInviteToAcceptedTrue = _repo.UpdateTeamInviteAccepted(id, true);
            return RedirectToAction("/Index");
        }
        [HttpGet]
        public ActionResult GetInvitesForLoggedInUser()
        {
            currentGamer = _repo.getgamerbyaspusername(User.Identity.Name);
            var query = from invites in _context.Invites.Where(o => o.InvitedGamer.GamerID == currentGamer.GamerID) select new { invites.Team.TeamName, invites.DateSent, invites.Accepted, invites.TeamInviteID };
            return Json(query.Distinct(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public bool DeclineInvite(int id)
        {
            bool DeclinedInvite = _repo.DeleteTeamInviteByID(id);
            return DeclinedInvite;
        }
        [HttpGet]
        public ActionResult GetUsersTeamInfo()
        {
            currentGamer = _repo.getgamerbyaspusername(User.Identity.Name);
            var query = from gamer in _context.Gamers.Where(o => o.GamerID == currentGamer.GamerID) select new { gamer.MainTeam.TeamName, gamer.MainTeam.LogoLink, gamer.MainTeam.Website, gamer.MainTeam.DateFounded };
            return Json(query.Distinct(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetUsersLadders()
        {
            currentGamer = _repo.getgamerbyaspusername(User.Identity.Name);
            var query = _context.Database.SqlQuery<int>("SELECT LadderId FROM dbo.LadderMainTeams WHERE MainTeamID =" + currentGamer.MainTeam.MainTeamID).ToList();
            List<Ladder> result = new List<Ladder>();
            for (int x = 0; x < query.Count; x++)
            {
                result.Add(_repo.GetLadderById(query[x]));
            }
            JsonConverterLadder converter = new JsonConverterLadder(result);
            List<JsonConverterLadder> JSONtoSend = converter.ConvertLadderList();
            return Json(JSONtoSend, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PopulateProfile()
        {
            currentGamer = _repo.getgamerbyaspusername(User.Identity.Name);
            var query = from gamer in _context.Gamers.Where(o => o.GamerID == currentGamer.GamerID) select gamer.MainTeam;
            var query2 = from invites in _context.Invites.Where(o => o.InvitedGamer.GamerID == currentGamer.GamerID) select invites;
            var query3 = _context.Database.SqlQuery<int>("SELECT LadderId FROM dbo.LadderMainTeams WHERE MainTeamID =" + currentGamer.MainTeam.MainTeamID).ToList();
            var query4 = _context.Database.SqlQuery<int>("SELECT LeagueId FROM dbo.LeagueMainTeams WHERE MainTeamID =" + currentGamer.MainTeam.MainTeamID).ToList();
            var query5 = from match in _context.Matches
                         where (match.Team1.MainTeamID == currentGamer.MainTeam.MainTeamID || 
                         match.Team2.MainTeamID == currentGamer.MainTeam.MainTeamID)
                         select match;
            ProfileJsonHelper profiledata = new ProfileJsonHelper();
            ProfileJSONTeam teamserializer = new ProfileJSONTeam(query.FirstOrDefault());
            JsonReturnInvites inviteserializer = new JsonReturnInvites(query2.ToList());
            JsonConverterMatches matchserializer = new JsonConverterMatches(query5.ToList());
            List<Ladder> result = new List<Ladder>();
            for (int x = 0; x < query3.Count; x++)
            {
                result.Add(_repo.GetLadderById(query3[x]));
            }
            List<League> Query4Return = new List<League>();
            for (int y = 0; y < query4.Count; y++)
            {
                Query4Return.Add(_repo.GetLeagueByID(query4[y]));
            }
            JsonConverterLeague leagueconverter = new JsonConverterLeague(Query4Return);
            List<JsonConverterLeague> leagueserialized = leagueconverter.SerializeLeagues();
            JsonConverterLadder converter = new JsonConverterLadder(result);
            List<JsonConverterLadder> ladderserializer = converter.ConvertLadderList();
            List<JsonConverterMatches> matchesserialized = matchserializer.SerializeMatches();
            profiledata.TeamInfo = teamserializer.Serialize();
            profiledata.Invites = inviteserializer.ReturnTeamInvitesAsJson();
            profiledata.Ladders = ladderserializer;
            profiledata.Leagues = leagueserialized;
            profiledata.Matches = matchesserialized;
            return Json(profiledata, JsonRequestBehavior.AllowGet);
        }
    }
}