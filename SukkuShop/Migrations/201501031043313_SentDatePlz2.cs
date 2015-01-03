namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SentDatePlz2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SentDate");
        }
    }
}
