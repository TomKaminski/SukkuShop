namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productdemandsLinked : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProductDemands", "ProductId");
            AddForeignKey("dbo.ProductDemands", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDemands", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductDemands", new[] { "ProductId" });
        }
    }
}
