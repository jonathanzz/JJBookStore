namespace JJBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0002 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "NickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "NickName", c => c.String(maxLength: 20));
        }
    }
}
