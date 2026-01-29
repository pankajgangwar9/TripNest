using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementSystem.Models.Business
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TourPackageId { get; set; }

        [ForeignKey("TourPackageId")]
        public virtual TourPackage TourPackage { get; set; }

        public DateTime BookingDate { get; set; }

        public BookingStatus Status { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Review Review { get; set; }

    }

    public enum BookingStatus
    {
        Pending = 0,
        Confirmed = 1,
        Rejected = 2
    }

}
