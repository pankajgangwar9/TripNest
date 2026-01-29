using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TourismManagementSystem.Models;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private TourismDbContext db = new TourismDbContext();

        public ActionResult Create(int tourId)
        {
            ViewBag.TourId = tourId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int tourId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
            {
                ModelState.AddModelError("", "Invalid rating");
                return View();
            }

            var review = new TourismManagementSystem.Models.Business.Review
            {
                TourPackageId = tourId,
                UserId = User.Identity.GetUserId(),
                Rating = rating,
                Comment = comment
            };

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("Details", "Tour", new { id = tourId });
        }
    }

}
