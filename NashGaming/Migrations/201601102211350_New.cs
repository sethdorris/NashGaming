namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Matches", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Matches", "DateTime");
        }
    }
}
