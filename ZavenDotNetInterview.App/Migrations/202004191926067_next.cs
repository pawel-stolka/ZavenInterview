//namespace ZavenDotNetInterview.App.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
    
//    public partial class next : DbMigration
//    {
//        public override void Up()
//        {
//            AlterColumn("dbo.Jobs", "Name", c => c.String());
//            DropColumn("dbo.Jobs", "Created");
//            DropColumn("dbo.Jobs", "Fails");
//            DropColumn("dbo.Jobs", "LastUpdatedAt");
//        }
        
//        public override void Down()
//        {
//            AddColumn("dbo.Jobs", "LastUpdatedAt", c => c.DateTime());
//            AddColumn("dbo.Jobs", "Fails", c => c.Int(nullable: false));
//            AddColumn("dbo.Jobs", "Created", c => c.DateTime(nullable: false));
//            AlterColumn("dbo.Jobs", "Name", c => c.String(nullable: false, maxLength: 25));
//        }
//    }
//}
