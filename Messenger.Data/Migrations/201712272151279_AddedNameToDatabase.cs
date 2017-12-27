namespace Messenger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "Name");
        }
    }
}
