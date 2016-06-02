namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class June2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainTeams", "LogoLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainTeams", "LogoLink");
        }
    }
}
