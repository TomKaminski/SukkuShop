namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductWeight_cd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Weight", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
