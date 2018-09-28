namespace ProductStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHashInsteadPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminAccounts", "Salt", c => c.Binary(nullable: false));
            AddColumn("dbo.AdminAccounts", "Key", c => c.Binary(nullable: false));
            DropColumn("dbo.AdminAccounts", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminAccounts", "Password", c => c.String(nullable: false));
            DropColumn("dbo.AdminAccounts", "Key");
            DropColumn("dbo.AdminAccounts", "Salt");
        }
    }
}
