namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
        }
    }
}
