using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models.Business;


namespace TourismManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        TourismDbContext db = new TourismDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactMessage msg)
        {
            msg.SentOn = DateTime.Now;
            db.ContactMessages.Add(msg);
            db.SaveChanges();

            ViewBag.Success = "Your message has been sent successfully!";
            return View();
        }

    }
}