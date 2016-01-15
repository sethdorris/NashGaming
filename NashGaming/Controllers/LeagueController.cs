using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NashGaming.Models;
using System.IO;
using System.Runtime.Serialization;

namespace NashGaming.Controllers
{
    public class LeagueController : Controller
    {
        public NashGamingContext _context { get; set; }
        public NashGamingRepository _repo { get; set; }

        public LeagueController()
        {
            _context = new NashGamingContext();
            _repo = new NashGamingRepository();
        }
        // GET: League
        public ActionResult Index()
        {
            List<League> l = _repo.GetAllLeagues();
            return View(l);
        }

        [HttpGet]
        public ActionResult JsonLeagues()
        {
            List<League> l = _repo.GetAllLeagues();
            return Json(l, JsonRequestBehavior.AllowGet);
        }
    }
}