namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friends1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "Accepted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "Accepted");
        }
    }
}
