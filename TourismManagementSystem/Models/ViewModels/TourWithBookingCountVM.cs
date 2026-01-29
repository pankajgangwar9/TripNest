using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models.ViewModels
{
    public class TourWithBookingCountVM
    {
        public int TourPackageId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int DurationDays { get; set; }

        public int MaxGroupSize { get; set; }

        public int BookingCount { get; set; }
    }
}