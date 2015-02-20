namespace SukkuShop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FreeShippingPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "FreeShippingPayment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FreeShippingPayment");
        }
    }
}
