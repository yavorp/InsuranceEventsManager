namespace InsuranceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false, unicode: false),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        AccidentDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        DamageLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mechanics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        WorkshopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workshops", t => t.WorkshopId, cascadeDelete: true)
                .Index(t => t.WorkshopId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MechanicId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mechanics", t => t.MechanicId, cascadeDelete: true)
                .Index(t => t.MechanicId);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mechanics", "WorkshopId", "dbo.Workshops");
            DropForeignKey("dbo.Tasks", "MechanicId", "dbo.Mechanics");
            DropIndex("dbo.Tasks", new[] { "MechanicId" });
            DropIndex("dbo.Mechanics", new[] { "WorkshopId" });
            DropTable("dbo.Workshops");
            DropTable("dbo.Tasks");
            DropTable("dbo.Mechanics");
            DropTable("dbo.Accidents");
        }
    }
}
