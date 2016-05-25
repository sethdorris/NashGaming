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
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}