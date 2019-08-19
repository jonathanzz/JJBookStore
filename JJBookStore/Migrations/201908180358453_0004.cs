namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0004 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchaseds",
                c => new
                    {
                        PurchasedID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Quantity = c.Int(nullable: false),
                        PurchasedPrice = c.Double(nullable: false),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        PurchasedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchasedID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchaseds", "UserID", "dbo.Users");
            DropForeignKey("dbo.Purchaseds", "BookID", "dbo.Books");
            DropIndex("dbo.Purchaseds", new[] { "UserID" });
            DropIndex("dbo.Purchaseds", new[] { "BookID" });
            DropTable("dbo.Purchaseds");
        }
    }
}
