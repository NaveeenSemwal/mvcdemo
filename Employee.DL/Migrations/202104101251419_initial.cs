namespace Employees.DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountriesMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TwoCharCountryCode = c.String(),
                        ThreeCharCountryCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(),
                        Gender = c.String(maxLength: 50, unicode: false),
                        Dob = c.DateTime(),
                        IdProofName = c.String(),
                        CountryId = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        Email = c.String(),
                        Mobile = c.String(),
                        UserPassword = c.String(),
                        IsAdmin = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CountriesMasters", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.TitleMasters", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.TitleId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.TitleMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TitleId", "dbo.TitleMasters");
            DropForeignKey("dbo.Employees", "CountryId", "dbo.CountriesMasters");
            DropIndex("dbo.Employees", new[] { "CountryId" });
            DropIndex("dbo.Employees", new[] { "TitleId" });
            DropTable("dbo.TitleMasters");
            DropTable("dbo.Employees");
            DropTable("dbo.CountriesMasters");
        }
    }
}
