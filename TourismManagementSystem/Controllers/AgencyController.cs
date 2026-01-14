using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Agency")]
    public class AgencyController : Controller
    {
        TourismDbContext db = new TourismDbContext();

        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create");

            return View(agency);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Agency agency)
        {
            agency.UserId = User.Identity.GetUserId();
            db.Agencies.Add(agency);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        // Show all bookings for this agency
        public ActionResult Bookings()
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create");

            var bookings = db.Bookings
                             .Include(b => b.TourPackage)
                             .Where(b => b.TourPackage.AgencyId == agency.AgencyId)
                             .ToList();

            return View(bookings);
        }

        // APPROVE booking
        public ActionResult ApproveBooking(int id)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            var booking = db.Bookings
                            .Include(b => b.TourPackage)
                            .FirstOrDefault(b => b.BookingId == id &&
                                                 b.TourPackage.AgencyId == agency.AgencyId);

            if (booking != null)
            {
                booking.Status = BookingStatus.Confirmed;
                db.SaveChanges();
            }

            return RedirectToAction("Bookings");
        }

        // REJECT booking
        public ActionResult RejectBooking(int id)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            var booking = db.Bookings
                            .Include(b => b.TourPackage)
                            .FirstOrDefault(b => b.BookingId == id &&
                                                 b.TourPackage.AgencyId == agency.AgencyId);

            if (booking != null)
            {
                booking.Status = BookingStatus.Rejected;
                db.SaveChanges();
            }

            return RedirectToAction("Bookings");
        }
    }
}
