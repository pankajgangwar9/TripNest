using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;
using System.Data.Entity;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Tourist")]
    public class TourController : Controller
    {
        TourismDbContext db = new TourismDbContext();

        public ActionResult Index()
        {
            var tours = db.TourPackages.Include("Agency").ToList();
            return View(tours);
        }
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var tour = db.TourPackages
                .Include("Agency")
                .Include("Reviews")
                .FirstOrDefault(t => t.TourPackageId == id);    

            if (tour == null) return HttpNotFound();

            return View(tour);
        }

    }
}
