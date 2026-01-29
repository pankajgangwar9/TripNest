namespace TourismManagementSystem.MigrationsBusiness
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBusiness : DbMigration
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
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AgencyId);
            
            CreateTable(
                "dbo.TourPackages",
                c => new
                    {
                        TourPackageId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DurationDays = c.Int(nullable: false),
                        MaxGroupSize = c.Int(nullable: false),
                        AgencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourPackageId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, cascadeDelete: true)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourPackageId = c.Int(nullable: false),
                        UserId = c.String(),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TourPackageId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        TourPackageId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Feedback_Id = c.Int(),
                        Review_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Feedbacks", t => t.Feedback_Id)
                .ForeignKey("dbo.Reviews", t => t.Review_Id)
                .ForeignKey("dbo.TourPackages", t => t.TourPackageId, cascadeDelete: true)
                .Index(t => t.TourPackageId)
                .Index(t => t.Feedback_Id)
                .Index(t => t.Review_Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Message = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        BookingId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentStatus = c.String(nullable: false),
                        PaymentMethod = c.String(nullable: false),
                        TransactionId = c.String(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        ContactMessageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Subject = c.String(),
                        Message = c.String(),
                        SentOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactMessageId);
            
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
            DropForeignKey("dbo.Bookings", "Review_Id", "dbo.Reviews");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "Feedback_Id", "dbo.Feedbacks");
            DropForeignKey("dbo.Reviews", "TourPackageId", "dbo.TourPackages");
            DropForeignKey("dbo.TourPackages", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.TourImages", new[] { "TourPackageId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "Review_Id" });
            DropIndex("dbo.Bookings", new[] { "Feedback_Id" });
            DropIndex("dbo.Bookings", new[] { "TourPackageId" });
            DropIndex("dbo.Reviews", new[] { "TourPackageId" });
            DropIndex("dbo.TourPackages", new[] { "AgencyId" });
            DropTable("dbo.TourImages");
            DropTable("dbo.ContactMessages");
            DropTable("dbo.Payments");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Bookings");
            DropTable("dbo.Reviews");
            DropTable("dbo.TourPackages");
            DropTable("dbo.Agencies");
        }
    }
}
