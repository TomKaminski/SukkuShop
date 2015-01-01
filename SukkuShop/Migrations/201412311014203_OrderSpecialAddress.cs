namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderSpecialAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Name", c => c.String());
            AddColumn("dbo.Orders", "Surname", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "Street", c => c.String());
            AddColumn("dbo.Orders", "Number", c => c.String());
            AddColumn("dbo.Orders", "PostalCode", c => c.String());
            AddColumn("dbo.Orders", "SpecialAddress", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SpecialAddress");
            DropColumn("dbo.Orders", "PostalCode");
            DropColumn("dbo.Orders", "Number");
            DropColumn("dbo.Orders", "Street");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "Surname");
            DropColumn("dbo.Orders", "Name");
        }
    }
}
