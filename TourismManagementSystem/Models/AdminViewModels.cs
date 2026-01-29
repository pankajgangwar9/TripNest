using System;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Models.AdminViewModels
{
    public class AdminBookingViewModel
    {
        public int BookingId { get; set; }
        public string TourTitle { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
    }
}