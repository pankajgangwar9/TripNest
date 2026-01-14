using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Agency")]
    public class TourPackageController : Controller
    {
        TourismDbContext db = new TourismDbContext();

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
            {
                return RedirectToAction("Create", "Agency");
            }

            // 🔒 BLOCK unapproved agencies
            if (!agency.IsApproved)
            {
                ViewBag.Errors = new[] { "Your agency is not approved yet by admin." };
                return View(tour);
            }

            // Assign agency to this tour
            tour.AgencyId = agency.AgencyId;

            if (ModelState.IsValid)
            {
                db.TourPackages.Add(tour);
                db.SaveChanges();
                return RedirectToAction("MyTours");
            }

            return View(tour);
        }


        public ActionResult MyTours()
        {
            var userId = User.Identity.GetUserId();

            // Step 1: Get Agency first
            var agency = db.Agencies.FirstOrDefault(a => a.UserId == userId);

            if (agency == null)
                return RedirectToAction("Create", "Agency");

            // Step 2: Use AgencyId (NOT navigation property)
            var tours = db.TourPackages
                          .Where(t => t.AgencyId == agency.AgencyId)
                          .ToList();

            return View(tours);
        }


    }
}
