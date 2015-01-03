namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserHints", c => c.String());
            DropColumn("dbo.Orders", "SpecialAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "SpecialAddress", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "UserHints");
        }
    }
}
