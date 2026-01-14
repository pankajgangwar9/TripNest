namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBusinessSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        AgencyId = c.Int(nullable: false, identity: true),
                        AgencyName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        ContactNumber = c.String(nullable: false),
                        ProfileImage = c.String(),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AgencyId);
            
            CreateTable(
                "dbo.TourPackages",
                c => new
                    {
                        TourPackageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DurationDays = c.Int(nullable: false),
                        MaxGroupSize = c.Int(nullable: false),
                        AvailableFrom = c.DateTime(nullable: false),
                        AvailableTo = c.DateTime(nullable: false),
                        AgencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourPackageId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, cascadeDelete: true)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false),
                        TourPackageId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TourPackageId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        BookingId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        BookingId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentStatus = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.TourImages",
                c => new
                    {
                        TourImageId = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(nullable: false),
                        TourPackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourImageId)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TourPackageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "TourPackageId", "dbo.TourPackages");
            DropForeignKey("dbo.Bookings", "TourPackageId", "dbo.TourPackages");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Feedbacks", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.TourPackages", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.TourImages", new[] { "TourPackageId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Feedbacks", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "TourPackageId" });
            DropIndex("dbo.TourPackages", new[] { "AgencyId" });
            DropTable("dbo.TourImages");
            DropTable("dbo.Payments");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Bookings");
            DropTable("dbo.TourPackages");
            DropTable("dbo.Agencies");
        }
    }
}
