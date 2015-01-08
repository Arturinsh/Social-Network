namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false),
                        IdImage = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.UsersGalleries", "IdComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsersGalleries", "IdComment", c => c.Int(nullable: false));
            DropTable("dbo.Comments");
        }
    }
}
