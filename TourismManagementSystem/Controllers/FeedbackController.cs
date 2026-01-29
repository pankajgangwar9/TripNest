using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TourismManagementSystem.Models;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private TourismDbContext db = new TourismDbContext();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ModelState.AddModelError("", "Feedback cannot be empty");
                return View();
            }

            var feedback = new Feedback
            {
                UserId = User.Identity.GetUserId(),
                Message = message
            };

            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            TempData["Success"] = "Thank you for your feedback!";
            return RedirectToAction("Index", "Home");
        }
    }
}
