namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "StockQty", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "StockQty");
        }
    }
}
