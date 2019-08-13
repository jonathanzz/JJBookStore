namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        AdminName = c.String(),
                        AmPassword = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        UserID = c.Int(),
                        Amount = c.Int(nullable: false),
                        Author = c.String(maxLength: 50),
                        price = c.Double(nullable: false),
                        img = c.String(),
                        uploadDate = c.DateTime(),
                        OnSell = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        NickName = c.String(maxLength: 20),
                        BirthDate = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Address = c.String(),
                        IsValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.ShopCarts",
                c => new
                    {
                        ShopCartId = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShopCartId)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopCarts", "UserID", "dbo.Users");
            DropForeignKey("dbo.ShopCarts", "BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "UserID", "dbo.Users");
            DropIndex("dbo.ShopCarts", new[] { "UserID" });
            DropIndex("dbo.ShopCarts", new[] { "BookID" });
            DropIndex("dbo.Books", new[] { "UserID" });
            DropTable("dbo.ShopCarts");
            DropTable("dbo.Users");
            DropTable("dbo.Books");
            DropTable("dbo.Admins");
        }
    }
}
