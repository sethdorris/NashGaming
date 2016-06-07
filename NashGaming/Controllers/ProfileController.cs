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
        public ProfileController()
        {
            _repo = new NashGamingRepository();
            _context = new NashGamingContext();
        }
        // GET: Profile
        //public ActionResult Index()
        //{
        //    Gamer g = _repo.GetGamerByAspUserName(User.Identity.Name);
        //    return View(g);
        //}
    }
}