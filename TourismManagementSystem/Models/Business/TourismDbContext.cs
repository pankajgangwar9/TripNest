using System.Data.Entity;

namespace TourismManagementSystem.Models.Business
{
    public class TourismDbContext : DbContext
    {
        public TourismDbContext() : base("TourismConnection") { }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // ✅ BUSINESS TABLES
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
