namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesOnly : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            AddColumn("dbo.Categories", "UpperCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "Categories_CategoryId", c => c.Int());
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "Categories_CategoryId");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Categories", "Categories_CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            DropColumn("dbo.Products", "SubCategoryId");
            DropTable("dbo.SubCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Promotion = c.Int(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
            AddColumn("dbo.Products", "SubCategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "Categories_CategoryId" });
            DropColumn("dbo.Products", "CategoryId");
            DropColumn("dbo.Categories", "Categories_CategoryId");
            DropColumn("dbo.Categories", "UpperCategoryId");
            CreateIndex("dbo.Products", "SubCategoryId");
            CreateIndex("dbo.SubCategories", "CategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "SubCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
