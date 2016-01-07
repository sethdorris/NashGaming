namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLeague : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "Team1_TeamID" });
            DropIndex("dbo.Matches", new[] { "Team2_TeamID" });
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        LeagueID = c.Int(nullable: false, identity: true),
                        LeagueName = c.String(),
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
            
            AddColumn("dbo.Matches", "League_LeagueID", c => c.Int());
            AlterColumn("dbo.Matches", "Team1_TeamID", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "Team2_TeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "League_LeagueID");
            CreateIndex("dbo.Matches", "Team1_TeamID");
            CreateIndex("dbo.Matches", "Team2_TeamID");
            AddForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues", "LeagueID");
            AddForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
            DropColumn("dbo.Matches", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "Time", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams");
            DropForeignKey("dbo.LeagueTeams", "Team_TeamID", "dbo.Teams");
            DropForeignKey("dbo.LeagueTeams", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues");
            DropIndex("dbo.LeagueTeams", new[] { "Team_TeamID" });
            DropIndex("dbo.LeagueTeams", new[] { "League_LeagueID" });
            DropIndex("dbo.Matches", new[] { "Team2_TeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_TeamID" });
            DropIndex("dbo.Matches", new[] { "League_LeagueID" });
            AlterColumn("dbo.Matches", "Team2_TeamID", c => c.Int());
            AlterColumn("dbo.Matches", "Team1_TeamID", c => c.Int());
            DropColumn("dbo.Matches", "League_LeagueID");
            DropTable("dbo.LeagueTeams");
            DropTable("dbo.Leagues");
            CreateIndex("dbo.Matches", "Team2_TeamID");
            CreateIndex("dbo.Matches", "Team1_TeamID");
            AddForeignKey("dbo.Matches", "Team2_TeamID", "dbo.Teams", "TeamID");
            AddForeignKey("dbo.Matches", "Team1_TeamID", "dbo.Teams", "TeamID");
        }
    }
}
