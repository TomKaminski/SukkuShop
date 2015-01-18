namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ReservedQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "IconName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IconName");
            DropColumn("dbo.Products", "ReservedQuantity");
        }
    }
}
