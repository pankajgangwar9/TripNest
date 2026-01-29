using System;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Models.ViewModels
{
    public class AdminBookingViewModel
    {
        public int BookingId { get; set; }
        public string TourTitle { get; set; } = string.Empty;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
    }
}