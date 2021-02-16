namespace WordApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBcontrols",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Text = c.String(),
                    Control_ID = c.String(),
                    User_ID = c.Int(nullable: false),
                    Checked = c.Boolean(nullable: false),
                    Users_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);

            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConfirmPassword = c.String(nullable: false),
                        Nickname = c.String(nullable: false, maxLength: 20),
                        Forename = c.String(),
                        Surname = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        EmailVerification = c.Boolean(nullable: false),
                        Role = c.String(),
                        UniqueCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Displayed_Name = c.String(),
                        Format_String = c.String(),
                        Format_Int = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBcontrols", "Users_Id", "dbo.Users");
            DropIndex("dbo.DBcontrols", new[] { "Users_Id" });
            DropTable("dbo.Formats");
            DropTable("dbo.Users");
            DropTable("dbo.DBcontrols");
        }
    }
}
