namespace TourismManagementSystem.Migrations
{
    using System.Data.Entity.Migrations;
    using TourismManagementSystem.Models;

    internal sealed class Configuration
        : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Identity data can be seeded here if needed
        }
    }
}
