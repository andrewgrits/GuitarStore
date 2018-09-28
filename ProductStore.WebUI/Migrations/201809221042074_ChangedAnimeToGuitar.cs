namespace ProductStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAnimeToGuitar : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Animes", newName: "Guitars");
            AlterColumn("dbo.AdminAccounts", "Login", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.AdminAccounts", "Password", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdminAccounts", "Password", c => c.String());
            AlterColumn("dbo.AdminAccounts", "Login", c => c.String());
            RenameTable(name: "dbo.Guitars", newName: "Animes");
        }
    }
}
