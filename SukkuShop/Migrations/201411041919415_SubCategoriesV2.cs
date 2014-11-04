namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoriesV2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubCategories", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "Categories_CategoryId" });
            RenameColumn(table: "dbo.SubCategories", name: "Categories_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.SubCategories", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubCategories", "CategoryId");
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            AlterColumn("dbo.SubCategories", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.SubCategories", name: "CategoryId", newName: "Categories_CategoryId");
            CreateIndex("dbo.SubCategories", "Categories_CategoryId");
            AddForeignKey("dbo.SubCategories", "Categories_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
