using System.Linq;
using System.Web.Mvc;
using TourismManagementSystem.Models;
using TourismManagementSystem.Models.Business;
using TourismManagementSystem.Models.AdminViewModels;
using System.Collections.Generic;
using System.Data.Entity;

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
            // Load bookings with related tour package
            var bookings = businessDb.Bookings
                                     .Include("TourPackage")
                                     .ToList();

            // Collect distinct user ids and load usernames in one query to avoid N+1 DB calls
            var userIds = bookings.Select(b => b.UserId).Distinct().ToList();
            var users = identityDb.Users
                                  .Where(u => userIds.Contains(u.Id))
                                  .ToDictionary(u => u.Id, u => u.UserName);

            // Map bookings to the admin view-model
            var model = bookings.Select(b => new TourismManagementSystem.Models.AdminViewModels.AdminBookingViewModel

            {
                BookingId = b.BookingId,
                TourTitle = b.TourPackage?.Title,
                UserId = b.UserId,
                UserName = users.ContainsKey(b.UserId) ? users[b.UserId] : "(unknown)",
                BookingDate = b.BookingDate,
                Status = b.Status
            }).ToList();

            return View(model);
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

        // GET: Admin/EditUser/5
        public ActionResult EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Users");

            var user = identityDb.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Models.ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = identityDb.Users.Find(model.Id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            identityDb.Entry(user).State = EntityState.Modified;
            identityDb.SaveChanges();

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
