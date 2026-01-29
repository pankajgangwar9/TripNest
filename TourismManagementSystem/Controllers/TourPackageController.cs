using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;
using TourismManagementSystem.Models.ViewModels;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Agency")]
    public class TourPackageController : Controller
    {
        private TourismDbContext db = new TourismDbContext();

        // ================= CREATE =================

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TourPackage tour)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            if (!agency.IsApproved)
            {
                ViewBag.Errors = new[] { "Your agency is not approved yet by admin." };
                return View(tour);
            }

            tour.AgencyId = agency.AgencyId;

            if (ModelState.IsValid)
            {
                db.TourPackages.Add(tour);
                db.SaveChanges();
                return RedirectToAction("MyTours");
            }

            return View(tour);
        }

        // ================= MY TOURS =================

        public ActionResult MyTours()
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            var bookingCounts = db.Bookings
                .GroupBy(b => b.TourPackageId)
                .Select(g => new
                {
                    TourPackageId = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var model = db.TourPackages
                .Where(t => t.AgencyId == agency.AgencyId)
                .ToList()
                .Select(t => new TourWithBookingCountVM
                {
                    TourPackageId = t.TourPackageId,
                    Title = t.Title,
                    Price = t.Price,
                    DurationDays = t.DurationDays,
                    MaxGroupSize = t.MaxGroupSize,
                    BookingCount = bookingCounts
                        .FirstOrDefault(b => b.TourPackageId == t.TourPackageId)?.Count ?? 0
                })
                .ToList();

            return View(model);
        }

        // ================= EDIT =================

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            var tour = db.TourPackages
                .FirstOrDefault(t => t.TourPackageId == id && t.AgencyId == agency.AgencyId);

            if (tour == null)
                return HttpNotFound();

            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourPackage tour)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            var existingTour = db.TourPackages
                .FirstOrDefault(t => t.TourPackageId == tour.TourPackageId
                                  && t.AgencyId == agency.AgencyId);

            if (existingTour == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                existingTour.Title = tour.Title;
                existingTour.Description = tour.Description;
                existingTour.Price = tour.Price;
                existingTour.DurationDays = tour.DurationDays;
                existingTour.MaxGroupSize = tour.MaxGroupSize;

                db.SaveChanges();
                return RedirectToAction("MyTours");
            }

            return View(tour);
        }

        // ================= DELETE =================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            var tour = db.TourPackages
                .FirstOrDefault(t => t.TourPackageId == id && t.AgencyId == agency.AgencyId);

            if (tour == null)
                return HttpNotFound();

            db.TourPackages.Remove(tour);
            db.SaveChanges();

            return RedirectToAction("MyTours");
        }
    }
}
