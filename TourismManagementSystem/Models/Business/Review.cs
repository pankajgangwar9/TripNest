using System;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Models.Business
{
    public class Review
    {
        public int Id { get; set; }

        public int TourPackageId { get; set; }
        public virtual TourPackage TourPackage { get; set; }

        // Store ONLY UserId (string)
        public string UserId { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
