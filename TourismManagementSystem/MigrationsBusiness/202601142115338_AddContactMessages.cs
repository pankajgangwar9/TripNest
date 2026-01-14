namespace TourismManagementSystem.MigrationsBusiness
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactMessages : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactMessages");
        }
    }
}
