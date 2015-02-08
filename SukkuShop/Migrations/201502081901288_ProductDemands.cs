namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductDemands : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDemands",
                c => new
                    {
                        DemandId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DemandId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductDemands");
        }
    }
}
