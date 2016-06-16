using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NashGaming.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using NashGaming.Helpers;

namespace NashGaming.Controllers
{
    public class CompetitionsController : Controller
    {
        public NashGamingContext _context { get; set; }
        public NashGamingRepository _repo { get; set; }

        public CompetitionsController()
        {
            _context = new NashGamingContext();
            _repo = new NashGamingRepository();
        }
        // GET: League
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JsonLeagues()
        {
            List<League> l = _repo.GetAllLeagues();
            JsonReturnLeagues JSONLeagues = new JsonReturnLeagues(l);
            List<JsonReturnLeagues> result = JSONLeagues.ReturnLeaguesAsJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditGamesPerWeek(int lid, int gpw)
        {
            _repo.UpdateLeagueGamesPerWeek(lid, gpw);
            List<League> l = _repo.GetAllLeagues();
            JsonReturnLeagues JSONLeagues = new JsonReturnLeagues(l);
            List<JsonReturnLeagues> result = JSONLeagues.ReturnLeaguesAsJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult JsonGetLeagueById(int id)
        {
            League l = _repo.GetLeagueByID(id);
            JsonReturnLeagues jsonreturner = new JsonReturnLeagues(l);
            JsonReturnLeagues result = jsonreturner.ReturnLeagueAsJson();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllLadders()
        {
            List<Ladder> l = _repo.GetAllLadders();
            JsonConverterLadder results = new JsonConverterLadder(l);
            List<JsonConverterLadder> value = results.ConvertLadderList();
            return Json(value, JsonRequestBehavior.AllowGet);        }
    }
}