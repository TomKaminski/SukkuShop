namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersEnhancement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentName = c.String(),
                        PaymentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.ShippingTypes",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        ShippingName = c.String(),
                        ShippingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ShippingId);
            
            AddColumn("dbo.OrderDetails", "SubTotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ShippingId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PaymentId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductsPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Orders", "ShippingId");
            CreateIndex("dbo.Orders", "PaymentId");
            AddForeignKey("dbo.Orders", "PaymentId", "dbo.PaymentTypes", "PaymentId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ShippingId", "dbo.ShippingTypes", "ShippingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShippingId", "dbo.ShippingTypes");
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.PaymentTypes");
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "ShippingId" });
            DropColumn("dbo.Orders", "TotalPrice");
            DropColumn("dbo.Orders", "ProductsPrice");
            DropColumn("dbo.Orders", "PaymentId");
            DropColumn("dbo.Orders", "ShippingId");
            DropColumn("dbo.OrderDetails", "SubTotalPrice");
            DropTable("dbo.ShippingTypes");
            DropTable("dbo.PaymentTypes");
        }
    }
}
