using System.Linq;
using System.Web.Mvc;
using TourismManagementSystem.Models;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext identityDb = new ApplicationDbContext();
        TourismDbContext businessDb = new TourismDbContext();

        public ActionResult Dashboard()
        {
            ViewBag.Users = identityDb.Users.Count();
            ViewBag.Agencies = businessDb.Agencies.Count();
            ViewBag.Tours = businessDb.TourPackages.Count();
            ViewBag.Bookings = businessDb.Bookings.Count();

            return View();
        }

        public ActionResult Users()
        {
            return View(identityDb.Users.ToList());
        }

        public ActionResult Agencies()
        {
            return View(businessDb.Agencies.ToList());
        }

        public ActionResult Bookings()
        {
            var data = businessDb.Bookings
                                 .Include("TourPackage")
                                 .ToList();
            return View(data);
        }

        public ActionResult DeleteUser(string id)
        {
            var user = identityDb.Users.Find(id);
            if (user != null)
            {
                identityDb.Users.Remove(user);
                identityDb.SaveChanges();
            }
            return RedirectToAction("Users");
        }
        public ActionResult ApproveAgency(int id)
        {
            var agency = businessDb.Agencies.Find(id);
            agency.IsApproved = true;
            businessDb.SaveChanges();
            return RedirectToAction("Agencies");
        }

        public ActionResult RejectAgency(int id)
        {
            var agency = businessDb.Agencies.Find(id);
            agency.IsApproved = false;
            businessDb.SaveChanges();
            return RedirectToAction("Agencies");
        }

    }
}
