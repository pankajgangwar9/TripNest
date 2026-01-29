using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;
using System.Data.Entity;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Tourist")]
    public class BookingController : Controller
    {
        TourismDbContext db = new TourismDbContext();

        public ActionResult Create(int id)
        {
            var tour = db.TourPackages
             .Include(t => t.Agency)
             .Include(t => t.Reviews)
             .FirstOrDefault(t => t.TourPackageId == id);
            return View(tour);
        }

        [HttpPost]
        public ActionResult Confirm(int tourPackageId)
        {
            var booking = new Booking
            {
                UserId = User.Identity.GetUserId(),
                TourPackageId = tourPackageId,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Pending // Fixed: use enum value, not string
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return RedirectToAction("MyBookings");
        }

        public ActionResult MyBookings()
        {
            var uid = User.Identity.GetUserId();
            var bookings = db.Bookings
                .Where(b => b.UserId == uid)
                .ToList();

            return View(bookings);
        }
    }
}
