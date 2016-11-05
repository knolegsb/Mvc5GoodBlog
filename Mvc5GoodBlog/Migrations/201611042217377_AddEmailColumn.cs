namespace Mvc5GoodBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Mail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Mail");
        }
    }
}
