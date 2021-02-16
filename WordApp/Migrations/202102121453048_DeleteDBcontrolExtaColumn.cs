namespace WordApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDBcontrolExtaColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DBcontrols", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DBcontrols", "User_ID", c => c.Int(nullable: false));
        }
    }
}
