using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;

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
        public ActionResult Details(int id)
        {
            var tour = db.TourPackages.Include("Agency")
                        .FirstOrDefault(t => t.TourPackageId == id);
            return View(tour);
        }
    }
}
