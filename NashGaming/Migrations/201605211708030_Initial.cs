namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LeagueTeams", newName: "TeamLeagues");
            DropForeignKey("dbo.Gamers", "MemberOf_TeamID", "dbo.Teams");
            DropIndex("dbo.Gamers", new[] { "Team_TeamID" });
            DropIndex("dbo.Gamers", new[] { "MemberOf_TeamID" });
            DropIndex("dbo.Posts", new[] { "Author_GamerID" });
            DropIndex("dbo.Teams", new[] { "Founder_GamerID" });
            RenameColumn(table: "dbo.Posts", name: "Author_GamerID", newName: "Author_Id");
            RenameColumn(table: "dbo.Teams", name: "Founder_GamerID", newName: "Founder_Id");
            DropPrimaryKey("dbo.TeamLeagues");
            AddColumn("dbo.AspNetUsers", "Platform", c => c.String());
            AddColumn("dbo.AspNetUsers", "TeamID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Team_TeamID", c => c.Int());
            AlterColumn("dbo.Posts", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Teams", "Founder_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.TeamLeagues", new[] { "Team_TeamID", "League_LeagueID" });
            CreateIndex("dbo.Teams", "Founder_Id");
            CreateIndex("dbo.AspNetUsers", "Team_TeamID");
            CreateIndex("dbo.Posts", "Author_Id");
            DropTable("dbo.Gamers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Gamers",
                c => new
                    {
                        GamerID = c.Int(nullable: false, identity: true),
                        RealUserID = c.String(),
                        Handle = c.String(),
                        Platform = c.String(),
                        Team_TeamID = c.Int(),
                        MemberOf_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.GamerID);
            
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_TeamID" });
            DropIndex("dbo.Teams", new[] { "Founder_Id" });
            DropPrimaryKey("dbo.TeamLeagues");
            AlterColumn("dbo.Teams", "Founder_Id", c => c.Int());
            AlterColumn("dbo.Posts", "Author_Id", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Team_TeamID");
            DropColumn("dbo.AspNetUsers", "TeamID");
            DropColumn("dbo.AspNetUsers", "Platform");
            AddPrimaryKey("dbo.TeamLeagues", new[] { "League_LeagueID", "Team_TeamID" });
            RenameColumn(table: "dbo.Teams", name: "Founder_Id", newName: "Founder_GamerID");
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "Author_GamerID");
            CreateIndex("dbo.Teams", "Founder_GamerID");
            CreateIndex("dbo.Posts", "Author_GamerID");
            CreateIndex("dbo.Gamers", "MemberOf_TeamID");
            CreateIndex("dbo.Gamers", "Team_TeamID");
            AddForeignKey("dbo.Gamers", "MemberOf_TeamID", "dbo.Teams", "TeamID");
            RenameTable(name: "dbo.TeamLeagues", newName: "LeagueTeams");
        }
    }
}
