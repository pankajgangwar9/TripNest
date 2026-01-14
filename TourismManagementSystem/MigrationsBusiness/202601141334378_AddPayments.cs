namespace TourismManagementSystem.MigrationsBusiness
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPayments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentMethod", c => c.String(nullable: false));
            AddColumn("dbo.Payments", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "TransactionId");
            DropColumn("dbo.Payments", "PaymentMethod");
        }
    }
}
