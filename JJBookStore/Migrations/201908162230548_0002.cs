namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopCarts", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.ShopCarts", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopCarts", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.ShopCarts", "Quantity");
        }
    }
}
