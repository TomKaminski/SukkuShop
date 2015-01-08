namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirmyOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "NazwaFirmy", c => c.String());
            AddColumn("dbo.Orders", "NIP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "NIP");
            DropColumn("dbo.Orders", "NazwaFirmy");
        }
    }
}
