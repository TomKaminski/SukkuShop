namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderprodprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProdPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProdPrice");
        }
    }
}
