namespace ProductStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePropToGuitar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guitars", "ImageData", c => c.Binary());
            AddColumn("dbo.Guitars", "ImageMimeType", c => c.String(maxLength: 50));
            AlterColumn("dbo.Guitars", "Category", c => c.String(nullable: false, maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guitars", "Category", c => c.String(nullable: false));
            DropColumn("dbo.Guitars", "ImageMimeType");
            DropColumn("dbo.Guitars", "ImageData");
        }
    }
}
