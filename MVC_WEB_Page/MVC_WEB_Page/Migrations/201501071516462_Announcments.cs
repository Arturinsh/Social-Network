namespace MVC_WEB_Page.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdReceiver = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 64),
                        Date = c.DateTime(nullable: false),
                        read = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Announcments");
        }
    }
}
