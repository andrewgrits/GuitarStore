namespace ProductStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminAccounts");
        }
    }
}
