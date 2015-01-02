namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Galleries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Image = c.String(nullable: false),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        IdComment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersGalleries");
        }
    }
}
