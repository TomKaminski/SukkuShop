namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NipString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "KontoFirmowe", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "NIP");
            DropColumn("dbo.AspNetUsers", "Nip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Nip", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "NIP", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "KontoFirmowe");
        }
    }
}
