namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pending : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        LeagueID = c.Int(nullable: false, identity: true),
                        LeagueName = c.String(),
                        Platform = c.String(),
                    })
                .PrimaryKey(t => t.LeagueID);
            
            CreateTable(
                "dbo.LeagueTeams",
                c => new
                    {
                        League_LeagueID = c.Int(nullable: false),
                        Team_TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.League_LeagueID, t.Team_TeamID })
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_TeamID, cascadeDelete: true)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Team_TeamID);
            
            AddColumn("dbo.Teams", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Handle", c => c.String());
            AddColumn("dbo.Matches", "League_LeagueID", c => c.Int());
            CreateIndex("dbo.Matches", "League_LeagueID");
            AddForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues", "LeagueID");
            DropColumn("dbo.Matches", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "Time", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.LeagueTeams", "Team_TeamID", "dbo.Teams");
            DropForeignKey("dbo.LeagueTeams", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues");
            DropIndex("dbo.LeagueTeams", new[] { "Team_TeamID" });
            DropIndex("dbo.LeagueTeams", new[] { "League_LeagueID" });
            DropIndex("dbo.Matches", new[] { "League_LeagueID" });
            DropColumn("dbo.Matches", "League_LeagueID");
            DropColumn("dbo.AspNetUsers", "Handle");
            DropColumn("dbo.Teams", "Active");
            DropTable("dbo.LeagueTeams");
            DropTable("dbo.Leagues");
        }
    }
}
