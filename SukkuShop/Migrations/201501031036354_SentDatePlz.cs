namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SentDatePlz : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "SentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "SentDate", c => c.DateTime(nullable: false));
        }
    }
}
