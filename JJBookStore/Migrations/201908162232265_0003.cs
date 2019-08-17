namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopCarts", "CreatedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopCarts", "CreatedTime");
        }
    }
}
