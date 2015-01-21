namespace SukkuShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rabat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rabat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rabat");
        }
    }
}
