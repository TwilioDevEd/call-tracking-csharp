namespace CallTracking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        City = c.String(),
                        State = c.String(),
                        LeadSourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeadSources", t => t.LeadSourceId, cascadeDelete: true)
                .Index(t => t.LeadSourceId);
            
            CreateTable(
                "dbo.LeadSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IncomingNumberNational = c.String(),
                        IncomingNumberInternational = c.String(),
                        ForwardingNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "LeadSourceId", "dbo.LeadSources");
            DropIndex("dbo.Leads", new[] { "LeadSourceId" });
            DropTable("dbo.LeadSources");
            DropTable("dbo.Leads");
        }
    }
}
