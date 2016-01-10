using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NashGaming.Models;

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
    }
}