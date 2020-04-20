namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobsupdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Fails", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Fails");
        }
    }
}
