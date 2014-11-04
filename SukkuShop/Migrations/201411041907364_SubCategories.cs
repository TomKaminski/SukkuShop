namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Promotion = c.Int(),
                        Categories_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.Categories_CategoryId)
                .Index(t => t.Categories_CategoryId);
            
            AddColumn("dbo.Products", "SubCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SubCategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "SubCategoryId", cascadeDelete: true);
            DropColumn("dbo.Products", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SubCategories", "Categories_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "Categories_CategoryId" });
            DropColumn("dbo.Products", "SubCategoryId");
            DropTable("dbo.SubCategories");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
