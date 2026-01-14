namespace TourismManagementSystem.MigrationsBusiness
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBusiness : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TourPackages", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.TourPackages", "Description", c => c.String());
            DropColumn("dbo.TourPackages", "AvailableFrom");
            DropColumn("dbo.TourPackages", "AvailableTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourPackages", "AvailableTo", c => c.DateTime());
            AddColumn("dbo.TourPackages", "AvailableFrom", c => c.DateTime());
            AlterColumn("dbo.TourPackages", "Description", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.TourPackages", "Title", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
