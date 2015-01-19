namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductLife : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Published", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "WrongModel", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "Producer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Producer", c => c.String());
            DropColumn("dbo.Products", "WrongModel");
            DropColumn("dbo.Products", "IsComplete");
            DropColumn("dbo.Products", "Published");
        }
    }
}
