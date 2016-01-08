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
        // GET: League
        public ActionResult Index()
        {
            return View();
        }
    }
}