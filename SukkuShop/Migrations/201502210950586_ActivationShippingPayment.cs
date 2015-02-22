namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivationShippingPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentTypes", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShippingTypes", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingTypes", "Active");
            DropColumn("dbo.PaymentTypes", "Active");
        }
    }
}
