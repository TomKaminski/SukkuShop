namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShippingMaxWeight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingTypes", "MaxWeight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingTypes", "MaxWeight");
        }
    }
}
