namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Challenges", "MainTeam_MainTeamID", "dbo.MainTeams");
            DropIndex("dbo.Challenges", new[] { "MainTeam_MainTeamID" });
            DropColumn("dbo.Challenges", "MainTeam_MainTeamID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Challenges", "MainTeam_MainTeamID", c => c.Int());
            CreateIndex("dbo.Challenges", "MainTeam_MainTeamID");
            AddForeignKey("dbo.Challenges", "MainTeam_MainTeamID", "dbo.MainTeams", "MainTeamID");
        }
    }
}
