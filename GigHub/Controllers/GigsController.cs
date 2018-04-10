using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GigsController()
        {
            _db = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _db.Genres.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel)
        {                      
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime,
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _db.Gigs.Add(gig);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}