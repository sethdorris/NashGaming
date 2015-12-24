namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forced_disable_cascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "Team1_TeamID" });
            DropIndex("dbo.Matches", new[] { "Team2_TeamID" });
            AlterColumn("dbo.Matches", "Team1_TeamID", c => c.Int());
            AlterColumn("dbo.Matches", "Team2_TeamID", c => c.Int());
            CreateIndex("dbo.Matches", "Team1_TeamID");
            CreateIndex("dbo.Matches", "Team2_TeamID");
            AddForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams", "TeamID");
            AddForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "Team2_TeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_TeamID" });
            AlterColumn("dbo.Matches", "Team2_TeamID", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "Team1_TeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "Team2_TeamID");
            CreateIndex("dbo.Matches", "Team1_TeamID");
            AddForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
        }
    }
}
