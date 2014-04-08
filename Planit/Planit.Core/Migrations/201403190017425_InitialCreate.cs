namespace Planit.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Depth = c.Int(nullable: false),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Project_ID", "dbo.Projects");
            DropIndex("dbo.Projects", new[] { "Project_ID" });
            DropTable("dbo.Projects");
        }
    }
}
