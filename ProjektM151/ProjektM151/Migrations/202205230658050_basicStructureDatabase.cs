namespace ProjektM151.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basicStructureDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Description = c.String(),
                        Semester = c.Int(nullable: false),
                        Examdate = c.DateTime(nullable: false),
                        GradesIndex = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SubjectId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Semester = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SubjectId)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Subjects", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subjects", "User_Id");
            AddForeignKey("dbo.Subjects", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Homework", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Homework", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Exams", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Homework", new[] { "User_Id" });
            DropIndex("dbo.Homework", new[] { "SubjectId" });
            DropIndex("dbo.Exams", new[] { "User_Id" });
            DropIndex("dbo.Exams", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "User_Id" });
            DropColumn("dbo.Subjects", "User_Id");
            DropTable("dbo.Homework");
            DropTable("dbo.Exams");
        }
    }
}
