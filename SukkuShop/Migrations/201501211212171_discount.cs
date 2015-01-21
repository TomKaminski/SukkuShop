namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Discount");
        }
    }
}
