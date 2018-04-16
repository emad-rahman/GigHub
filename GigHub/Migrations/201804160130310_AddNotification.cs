namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OriginalDateTime = c.DateTime(),
                        OriginalVenue = c.String(),
                        Gig_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gigs", t => t.Gig_Id, cascadeDelete: true)
                .Index(t => t.Gig_Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NotifiactionId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Notification_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotifiactionId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Notifications", t => t.Notification_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Notification_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.UserNotifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "Gig_Id", "dbo.Gigs");
            DropIndex("dbo.UserNotifications", new[] { "Notification_Id" });
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Notifications", new[] { "Gig_Id" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
