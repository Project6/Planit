namespace Planit.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blah : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Project_ID", "dbo.Projects");
            DropIndex("dbo.Projects", new[] { "Project_ID" });
            AddColumn("dbo.Projects", "ParentID", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "ParentTitle", c => c.String());
            AddColumn("dbo.Projects", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "Note", c => c.String());
            AddColumn("dbo.Projects", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "ChildrenStr", c => c.String());
            DropColumn("dbo.Projects", "Description");
            DropColumn("dbo.Projects", "Project_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Project_ID", c => c.Int());
            AddColumn("dbo.Projects", "Description", c => c.String());
            DropColumn("dbo.Projects", "ChildrenStr");
            DropColumn("dbo.Projects", "Status");
            DropColumn("dbo.Projects", "Note");
            DropColumn("dbo.Projects", "Title");
            DropColumn("dbo.Projects", "ParentTitle");
            DropColumn("dbo.Projects", "ParentID");
            CreateIndex("dbo.Projects", "Project_ID");
            AddForeignKey("dbo.Projects", "Project_ID", "dbo.Projects", "ID");
        }
    }
}
