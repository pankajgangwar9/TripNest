using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TourismManagementSystem.Models.Business;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace TourismManagementSystem.Controllers
{
    [Authorize(Roles = "Tourist")]
    public class PaymentController : Controller
    {
        TourismDbContext db = new TourismDbContext();

        public ActionResult Pay(int bookingId)
        {
            var booking = db.Bookings.Find(bookingId);
            return View(booking);
        }

        [HttpPost]
        public ActionResult Pay(int bookingId, string paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod))
            {
                ViewBag.Error = "Please select a payment method";
                return View(db.Bookings.Include("TourPackage")
                                      .FirstOrDefault(b => b.BookingId == bookingId));
            }

            var booking = db.Bookings
                            .Include("TourPackage")
                            .FirstOrDefault(b => b.BookingId == bookingId);

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = booking.TourPackage.Price,
                PaymentDate = DateTime.Now,
                PaymentStatus = "Paid",        // <-- required field set
                PaymentMethod = paymentMethod,
                TransactionId = Guid.NewGuid().ToString()
            };

            booking.Status = BookingStatus.Confirmed;

            db.Payments.Add(payment);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var ev in ex.EntityValidationErrors)
                    foreach (var ve in ev.ValidationErrors)
                        System.Diagnostics.Debug.WriteLine($"{ve.PropertyName}: {ve.ErrorMessage}");
                throw;
            }

            return RedirectToAction("MyBookings", "Booking");
        }


    }
}