namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Categories_CategoryId" });
            AlterColumn("dbo.Categories", "UpperCategoryId", c => c.Int());
            DropColumn("dbo.Categories", "Categories_CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Categories_CategoryId", c => c.Int());
            AlterColumn("dbo.Categories", "UpperCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "Categories_CategoryId");
            AddForeignKey("dbo.Categories", "Categories_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
