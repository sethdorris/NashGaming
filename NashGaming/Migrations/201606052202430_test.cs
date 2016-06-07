namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Challenges",
                c => new
                    {
                        ChallengeID = c.Int(nullable: false, identity: true),
                        ProposedDate1 = c.DateTime(nullable: false),
                        ProposedDate2 = c.DateTime(nullable: false),
                        ProposedDate3 = c.DateTime(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        SubTeam_SubTeamID = c.Int(),
                        Initiator_SubTeamID = c.Int(nullable: false),
                        Ladder_LadderID = c.Int(nullable: false),
                        Recipient_SubTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChallengeID)
                .ForeignKey("dbo.SubTeams", t => t.SubTeam_SubTeamID)
                .ForeignKey("dbo.SubTeams", t => t.Initiator_SubTeamID, cascadeDelete: true)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID, cascadeDelete: true)
                .ForeignKey("dbo.SubTeams", t => t.Recipient_SubTeamID, cascadeDelete: true)
                .Index(t => t.SubTeam_SubTeamID)
                .Index(t => t.Initiator_SubTeamID)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.Recipient_SubTeamID);
            
            CreateTable(
                "dbo.SubTeams",
                c => new
                    {
                        SubTeamID = c.Int(nullable: false, identity: true),
                        SubTeamName = c.String(),
                        Rank = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        League_LeagueID = c.Int(),
                        Ladder_LadderID = c.Int(),
                        MainTeam_MainTeamID = c.Int(),
                        Gamer_GamerID = c.Int(),
                        TeamInvite_TeamInviteID = c.Int(),
                        Captain_GamerID = c.Int(),
                    })
                .PrimaryKey(t => t.SubTeamID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.MainTeams", t => t.MainTeam_MainTeamID)
                .ForeignKey("dbo.Gamers", t => t.Gamer_GamerID)
                .ForeignKey("dbo.TeamInvites", t => t.TeamInvite_TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.Captain_GamerID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.MainTeam_MainTeamID)
                .Index(t => t.Gamer_GamerID)
                .Index(t => t.TeamInvite_TeamInviteID)
                .Index(t => t.Captain_GamerID);
            
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
                        SubTeam_SubTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.GamerID)
                .ForeignKey("dbo.SubTeams", t => t.SubTeam_SubTeamID)
                .Index(t => t.SubTeam_SubTeamID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostsID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_GamerID = c.Int(nullable: false),
                        Ladder_LadderID = c.Int(),
                        League_LeagueID = c.Int(),
                    })
                .PrimaryKey(t => t.PostsID)
                .ForeignKey("dbo.Gamers", t => t.Author_GamerID, cascadeDelete: true)
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
                        Team1_SubTeamID = c.Int(nullable: false),
                        Team2_SubTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .ForeignKey("dbo.SubTeams", t => t.Team1_SubTeamID, cascadeDelete: true)
                .ForeignKey("dbo.SubTeams", t => t.Team2_SubTeamID, cascadeDelete: true)
                .Index(t => t.Ladder_LadderID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Team1_SubTeamID)
                .Index(t => t.Team2_SubTeamID);
            
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
                "dbo.MainTeams",
                c => new
                    {
                        MainTeamID = c.Int(nullable: false),
                        TeamName = c.String(),
                        DateFounded = c.DateTime(nullable: false),
                        Website = c.String(),
                        Active = c.Boolean(nullable: false),
                        LogoLink = c.String(),
                    })
                .PrimaryKey(t => t.MainTeamID)
                .ForeignKey("dbo.Gamers", t => t.MainTeamID)
                .Index(t => t.MainTeamID);
            
            CreateTable(
                "dbo.TeamInvites",
                c => new
                    {
                        TeamInviteID = c.Int(nullable: false, identity: true),
                        DateSent = c.DateTime(nullable: false),
                        DateAccepted = c.DateTime(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        InvitedGamer_GamerID = c.Int(nullable: false),
                        Team_MainTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.InvitedGamer_GamerID, cascadeDelete: true)
                .ForeignKey("dbo.MainTeams", t => t.Team_MainTeamID, cascadeDelete: true)
                .Index(t => t.InvitedGamer_GamerID)
                .Index(t => t.Team_MainTeamID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Challenges", "Recipient_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Challenges", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Challenges", "Initiator_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Gamers", "SubTeam_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Challenges", "SubTeam_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.SubTeams", "Captain_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.TeamInvites", "Team_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.SubTeams", "TeamInvite_TeamInviteID", "dbo.TeamInvites");
            DropForeignKey("dbo.TeamInvites", "InvitedGamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.SubTeams", "Gamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.SubTeams", "MainTeam_MainTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.MainTeams", "MainTeamID", "dbo.Gamers");
            DropForeignKey("dbo.SubTeams", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Matches", "Team2_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Matches", "Team1_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.SubTeams", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Author_GamerID", "dbo.Gamers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TeamInvites", new[] { "Team_MainTeamID" });
            DropIndex("dbo.TeamInvites", new[] { "InvitedGamer_GamerID" });
            DropIndex("dbo.MainTeams", new[] { "MainTeamID" });
            DropIndex("dbo.Matches", new[] { "Team2_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "League_LeagueID" });
            DropIndex("dbo.Matches", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "League_LeagueID" });
            DropIndex("dbo.Posts", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "Author_GamerID" });
            DropIndex("dbo.Gamers", new[] { "SubTeam_SubTeamID" });
            DropIndex("dbo.SubTeams", new[] { "Captain_GamerID" });
            DropIndex("dbo.SubTeams", new[] { "TeamInvite_TeamInviteID" });
            DropIndex("dbo.SubTeams", new[] { "Gamer_GamerID" });
            DropIndex("dbo.SubTeams", new[] { "MainTeam_MainTeamID" });
            DropIndex("dbo.SubTeams", new[] { "Ladder_LadderID" });
            DropIndex("dbo.SubTeams", new[] { "League_LeagueID" });
            DropIndex("dbo.Challenges", new[] { "Recipient_SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Challenges", new[] { "Initiator_SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "SubTeam_SubTeamID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TeamInvites");
            DropTable("dbo.MainTeams");
            DropTable("dbo.Leagues");
            DropTable("dbo.Matches");
            DropTable("dbo.Ladders");
            DropTable("dbo.Posts");
            DropTable("dbo.Gamers");
            DropTable("dbo.SubTeams");
            DropTable("dbo.Challenges");
        }
    }
}
