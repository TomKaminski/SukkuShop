namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NipString2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderNip", c => c.String());
            AddColumn("dbo.AspNetUsers", "AccNip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccNip");
            DropColumn("dbo.Orders", "OrderNip");
        }
    }
}
