using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GigHub.Controllers
{    
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now)
                .ToList();

            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}