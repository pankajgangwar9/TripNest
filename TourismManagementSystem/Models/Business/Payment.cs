using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementSystem.Models.Business
{
    public class Payment
    {
        [Key, ForeignKey("Booking")]
        public int BookingId { get; set; }

        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        public virtual Booking Booking { get; set; }
        [Required]
        public string PaymentMethod { get; set; }   // Card, UPI, Cash, etc

        public string TransactionId { get; set; }
    }
}
