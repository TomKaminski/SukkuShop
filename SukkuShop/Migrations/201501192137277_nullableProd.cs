namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableProd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.Products", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Quantity", c => c.Int());
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
