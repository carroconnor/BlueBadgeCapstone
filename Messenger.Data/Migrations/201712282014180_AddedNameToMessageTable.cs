namespace Messenger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToMessageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "Name_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Message", "Name_Id");
            AddForeignKey("dbo.Message", "Name_Id", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "Name_Id", "dbo.ApplicationUser");
            DropIndex("dbo.Message", new[] { "Name_Id" });
            DropColumn("dbo.Message", "Name_Id");
        }
    }
}
