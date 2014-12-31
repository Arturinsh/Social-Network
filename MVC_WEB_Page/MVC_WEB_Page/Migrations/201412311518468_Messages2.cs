namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Messages2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "read", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "read");
        }
    }
}
