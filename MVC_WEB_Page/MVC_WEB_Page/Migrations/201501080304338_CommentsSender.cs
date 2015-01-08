namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsSender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "SenderName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "SenderName");
        }
    }
}
