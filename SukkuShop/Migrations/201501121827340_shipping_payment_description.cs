namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipping_payment_description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentTypes", "PaymentDescription", c => c.String());
            AddColumn("dbo.ShippingTypes", "ShippingDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingTypes", "ShippingDescription");
            DropColumn("dbo.PaymentTypes", "PaymentDescription");
        }
    }
}
