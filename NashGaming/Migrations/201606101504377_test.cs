namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LadderMainTeams", newName: "LadderMainTeam1");
            RenameTable(name: "dbo.LeagueMainTeams", newName: "LeagueMainTeam1");
            CreateTable(
                "dbo.LadderMainTeams",
                c => new
                    {
                        Ladder_LadderID = c.Int(nullable: false),
                        MainTeam_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ladder_LadderID, t.MainTeam_MainTeamID });
            
            CreateTable(
                "dbo.LeagueMainTeams",
                c => new
                    {
                        League_LeagueID = c.Int(nullable: false),
                        MainTeam_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.League_LeagueID, t.MainTeam_MainTeamID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LeagueMainTeams");
            DropTable("dbo.LadderMainTeams");
            RenameTable(name: "dbo.LeagueMainTeam1", newName: "LeagueMainTeams");
            RenameTable(name: "dbo.LadderMainTeam1", newName: "LadderMainTeams");
        }
    }
}
