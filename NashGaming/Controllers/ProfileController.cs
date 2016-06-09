using NashGaming.Models;
using System;
using System.Collections.Generic;
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
            return RedirectToRoute("/Profile");
        }
        [HttpGet]
        public ActionResult GetInvitesForLoggedInUser()
        {
            currentGamer = _repo.getgamerbyaspusername(User.Identity.Name);
            List<TeamInvite> allmyinvites = _repo.GetTeamInvitesByGamerID(currentGamer.GamerID);
            JsonReturnInvites helper = new JsonReturnInvites(allmyinvites);
            List<JsonReturnInvites> result = helper.ReturnTeamInvitesAsJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}