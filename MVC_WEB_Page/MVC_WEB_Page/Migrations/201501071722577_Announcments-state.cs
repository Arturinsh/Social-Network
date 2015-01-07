namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcmentsstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcments", "state", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcments", "state");
        }
    }
}
