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
    }
}