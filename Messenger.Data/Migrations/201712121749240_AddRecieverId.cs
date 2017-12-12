namespace Messenger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecieverId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "RecieverId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "RecieverId");
        }
    }
}
