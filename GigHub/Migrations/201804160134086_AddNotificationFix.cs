namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserNotifications", "UserId");
            RenameColumn(table: "dbo.UserNotifications", name: "ApplicationUser_Id", newName: "UserId");
            DropPrimaryKey("dbo.UserNotifications");
            AlterColumn("dbo.UserNotifications", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotifiactionId" });
            CreateIndex("dbo.UserNotifications", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropPrimaryKey("dbo.UserNotifications");
            AlterColumn("dbo.UserNotifications", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotifiactionId" });
            RenameColumn(table: "dbo.UserNotifications", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.UserNotifications", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserNotifications", "ApplicationUser_Id");
        }
    }
}
