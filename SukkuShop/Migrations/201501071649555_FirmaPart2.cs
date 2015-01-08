namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirmaPart2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NazwaFirmy", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Nip");
            DropColumn("dbo.AspNetUsers", "NazwaFirmy");
        }
    }
}
