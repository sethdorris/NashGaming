namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Challenges",
                c => new
                    {
                        ChallengeID = c.Int(nullable: false, identity: true),
                        InitiatorId = c.Int(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        ProposedDate1 = c.DateTime(nullable: false),
                        ProposedDate2 = c.DateTime(nullable: false),
                        ProposedDate3 = c.DateTime(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        Ladder_LadderID = c.Int(),
                    })
                .PrimaryKey(t => t.ChallengeID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.MainTeams", t => t.InitiatorId, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.RecipientId, cascadeDelete: false)
                .Index(t => t.InitiatorId)
                .Index(t => t.RecipientId)
                .Index(t => t.Ladder_LadderID);
            
            CreateTable(
                "dbo.MainTeams",
                c => new
                    {
                        MainTeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        DateFounded = c.DateTime(nullable: false),
                        Website = c.String(),
                        Active = c.Boolean(nullable: false),
                        LogoLink = c.String(),
                    })
                .PrimaryKey(t => t.MainTeamID);
            
            CreateTable(
                "dbo.Gamers",
                c => new
                    {
                        GamerID = c.Int(nullable: false, identity: true),
                        RealUserID = c.String(),
                        Email = c.String(),
                        XB1Gamertag = c.String(),
                        PSNID = c.String(),
                        DisplayName = c.String(),
                        Active = c.Boolean(nullable: false),
                        MainTeam_MainTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.GamerID)
                .ForeignKey("dbo.MainTeams", t => t.MainTeam_MainTeamID)
                .Index(t => t.MainTeam_MainTeamID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostsID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        Author_GamerID = c.Int(),
                        Ladder_LadderID = c.Int(),
                        League_LeagueID = c.Int(),
                    })
                .PrimaryKey(t => t.PostsID)
                .ForeignKey("dbo.Gamers", t => t.Author_GamerID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .Index(t => t.Author_GamerID)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.League_LeagueID);
            
            CreateTable(
                "dbo.Ladders",
                c => new
                    {
                        LadderID = c.Int(nullable: false, identity: true),
                        LadderName = c.String(),
                        GameTitle = c.String(),
                        MinPlayers = c.Int(nullable: false),
                        MaxPlayers = c.Int(nullable: false),
                        Platform = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LadderID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        DatePlayed = c.DateTime(nullable: false),
                        Result = c.String(),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Ladder_LadderID = c.Int(),
                        League_LeagueID = c.Int(),
                        Team1_MainTeamID = c.Int(nullable: false),
                        Team2_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .ForeignKey("dbo.MainTeams", t => t.Team1_MainTeamID, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.Team2_MainTeamID, cascadeDelete: false)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Team1_MainTeamID)
                .Index(t => t.Team2_MainTeamID);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        LeagueID = c.Int(nullable: false, identity: true),
                        MinPlayers = c.Int(nullable: false),
                        MaxPlayers = c.Int(nullable: false),
                        LeagueName = c.String(),
                        GamesPerWeek = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SeasonLength = c.Int(nullable: false),
                        LeagueType = c.String(),
                        GameTitle = c.String(),
                        Platform = c.String(maxLength: 3),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LeagueID);
            
            CreateTable(
                "dbo.TeamInvites",
                c => new
                    {
                        TeamInviteID = c.Int(nullable: false, identity: true),
                        DateSent = c.DateTime(nullable: false),
                        DateAccepted = c.DateTime(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        InvitedGamer_GamerID = c.Int(),
                        Team_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.InvitedGamer_GamerID)
                .ForeignKey("dbo.MainTeams", t => t.Team_MainTeamID, cascadeDelete: false)
                .Index(t => t.InvitedGamer_GamerID)
                .Index(t => t.Team_MainTeamID);
            
            CreateTable(
                "dbo.LadderMainTeams",
                c => new
                    {
                        LadderId = c.Int(nullable: false),
                        MainTeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LadderId, t.MainTeamId })
                .ForeignKey("dbo.Leagues", t => t.LadderId, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.MainTeamId, cascadeDelete: false)
                .Index(t => t.LadderId)
                .Index(t => t.MainTeamId);
            
            CreateTable(
                "dbo.LeagueMainTeams",
                c => new
                    {
                        LeagueId = c.Int(nullable: false),
                        MainTeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LeagueId, t.MainTeamId })
                .ForeignKey("dbo.Leagues", t => t.LeagueId, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.MainTeamId, cascadeDelete: false)
                .Index(t => t.LeagueId)
                .Index(t => t.MainTeamId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        XbxGamertag = c.String(),
                        PSNID = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LadderMainTeams1",
                c => new
                    {
                        Ladder_LadderID = c.Int(nullable: false),
                        MainTeam_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ladder_LadderID, t.MainTeam_MainTeamID })
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.MainTeam_MainTeamID, cascadeDelete: false)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.MainTeam_MainTeamID);
            
            CreateTable(
                "dbo.LeagueMainTeams1",
                c => new
                    {
                        League_LeagueID = c.Int(nullable: false),
                        MainTeam_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.League_LeagueID, t.MainTeam_MainTeamID })
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.MainTeam_MainTeamID, cascadeDelete: false)
                .Index(t => t.League_LeagueID)
                .Index(t => t.MainTeam_MainTeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LeagueMainTeams", "MainTeamId", "dbo.MainTeams");
            DropForeignKey("dbo.LeagueMainTeams", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.LadderMainTeams", "MainTeamId", "dbo.MainTeams");
            DropForeignKey("dbo.LadderMainTeams", "LadderId", "dbo.Leagues");
            DropForeignKey("dbo.Challenges", "RecipientId", "dbo.MainTeams");
            DropForeignKey("dbo.Challenges", "InitiatorId", "dbo.MainTeams");
            DropForeignKey("dbo.TeamInvites", "Team_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.TeamInvites", "InvitedGamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.Gamers", "MainTeam_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.Matches", "Team2_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.Matches", "Team1_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.LeagueMainTeams1", "MainTeam_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.LeagueMainTeams1", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.LadderMainTeams1", "MainTeam_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.LadderMainTeams1", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Challenges", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Author_GamerID", "dbo.Gamers");
            DropIndex("dbo.LeagueMainTeams1", new[] { "MainTeam_MainTeamID" });
            DropIndex("dbo.LeagueMainTeams1", new[] { "League_LeagueID" });
            DropIndex("dbo.LadderMainTeams1", new[] { "MainTeam_MainTeamID" });
            DropIndex("dbo.LadderMainTeams1", new[] { "Ladder_LadderID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LeagueMainTeams", new[] { "MainTeamId" });
            DropIndex("dbo.LeagueMainTeams", new[] { "LeagueId" });
            DropIndex("dbo.LadderMainTeams", new[] { "MainTeamId" });
            DropIndex("dbo.LadderMainTeams", new[] { "LadderId" });
            DropIndex("dbo.TeamInvites", new[] { "Team_MainTeamID" });
            DropIndex("dbo.TeamInvites", new[] { "InvitedGamer_GamerID" });
            DropIndex("dbo.Matches", new[] { "Team2_MainTeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_MainTeamID" });
            DropIndex("dbo.Matches", new[] { "League_LeagueID" });
            DropIndex("dbo.Matches", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "League_LeagueID" });
            DropIndex("dbo.Posts", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "Author_GamerID" });
            DropIndex("dbo.Gamers", new[] { "MainTeam_MainTeamID" });
            DropIndex("dbo.Challenges", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Challenges", new[] { "RecipientId" });
            DropIndex("dbo.Challenges", new[] { "InitiatorId" });
            DropTable("dbo.LeagueMainTeams1");
            DropTable("dbo.LadderMainTeams1");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LeagueMainTeams");
            DropTable("dbo.LadderMainTeams");
            DropTable("dbo.TeamInvites");
            DropTable("dbo.Leagues");
            DropTable("dbo.Matches");
            DropTable("dbo.Ladders");
            DropTable("dbo.Posts");
            DropTable("dbo.Gamers");
            DropTable("dbo.MainTeams");
            DropTable("dbo.Challenges");
        }
    }
}
