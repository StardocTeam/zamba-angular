namespace Zamba.WebAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZInformation",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Repeat = c.Int(nullable: false),
                    Important = c.Int(nullable: false),
                    ShortContent = c.String(),
                    FullContent = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ZInformationUser",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Long(nullable: false),
                    ZInformationId = c.Int(nullable: false),
                    Readed = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ZInformation", t => t.ZInformationId, cascadeDelete: true)
                .Index(t => t.ZInformationId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ZInformationUser", "ZInformationId", "dbo.ZInformation");
            DropIndex("dbo.ZInformationUser", new[] { "ZInformationId" });
            DropTable("dbo.ZInformationUser");
            DropTable("dbo.ZInformation");
        }
    }
}
