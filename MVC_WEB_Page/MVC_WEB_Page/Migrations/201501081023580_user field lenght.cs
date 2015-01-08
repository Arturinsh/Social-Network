namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userfieldlenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
