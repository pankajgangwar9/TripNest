namespace TourismManagementSystem.MigrationsBusiness
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgencyApproval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "IsApproved");
        }
    }
}
