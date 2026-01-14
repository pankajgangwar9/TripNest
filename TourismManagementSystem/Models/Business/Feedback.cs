using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementSystem.Models.Business
{
    public class Feedback
    {
        [Key, ForeignKey("Booking")]
        public int BookingId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
