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
    }
}