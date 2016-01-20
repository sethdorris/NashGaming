using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NashGaming.Models;
using Microsoft.AspNet.Identity;

namespace NashGaming.Controllers
{
    public class HomeController : Controller
    {
        public NashGamingContext _context { get; set; }
        public NashGamingRepository _repo { get; set; }

        public HomeController()
        {
            _context = new NashGamingContext();
            _repo = new NashGamingRepository();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            List<Gamer> gamers = _repo.GetAllGamers();

            return View(gamers);
        }

        public ActionResult Contact()
        {
            string user_id = User.Identity.GetUserId();
            Gamer me = _repo.GetAllGamers().Where(o => o.RealUser.Id == user_id).Single();

            return View(me);
        }
    }
}