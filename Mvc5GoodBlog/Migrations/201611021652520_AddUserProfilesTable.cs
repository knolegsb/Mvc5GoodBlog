namespace Mvc5GoodBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfilesTable : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Slug = c.String(nullable: false),
            //            PostCount = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Posts",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Slug = c.String(nullable: false),
            //            Content = c.String(nullable: false),
            //            Created = c.DateTime(nullable: false),
            //            CategoryId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Users", t => t.UserId)
            //    .ForeignKey("dbo.Categories", t => t.CategoryId)
            //    .Index(t => t.CategoryId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Comments",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Username = c.String(nullable: false),
            //            Name = c.String(nullable: false),
            //            Content = c.String(nullable: false),
            //            Created = c.DateTime(nullable: false),
            //            PostId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Posts", t => t.PostId)
            //    .Index(t => t.PostId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Username = c.String(nullable: false),
            //            Password = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            //DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            //DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            //DropIndex("dbo.Comments", new[] { "PostId" });
            //DropIndex("dbo.Posts", new[] { "UserId" });
            //DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("dbo.UserProfiles");
            //DropTable("dbo.Users");
            //DropTable("dbo.Comments");
            //DropTable("dbo.Posts");
            //DropTable("dbo.Categories");
        }
    }
}
