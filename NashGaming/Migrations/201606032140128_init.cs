namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        LadderID = c.Int(nullable: false),
                        Initiator_SubTeamID = c.Int(nullable: false),
                        Recipient_SubTeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChallengeID)
                .ForeignKey("dbo.Ladders", t => t.LadderID, cascadeDelete: false)
                .ForeignKey("dbo.SubTeams", t => t.Initiator_SubTeamID, cascadeDelete: false)
                .ForeignKey("dbo.SubTeams", t => t.Recipient_SubTeamID, cascadeDelete: false)
                .Index(t => t.LadderID)
                .Index(t => t.Initiator_SubTeamID)
                .Index(t => t.Recipient_SubTeamID);
            
            CreateTable(
                "dbo.SubTeams",
                c => new
                    {
                        SubTeamID = c.Int(nullable: false),
                        SubTeamName = c.String(),
                        MainTeamID = c.Int(nullable: false),
                        CaptainID = c.Int(nullable: false),
                        LeagueID = c.Int(nullable: true),
                        LadderID = c.Int(nullable: true),
                        Rank = c.Int(nullable: true),
                        Points = c.Int(nullable: true),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SubTeamID)
                .ForeignKey("dbo.Leagues", t => t.LeagueID, cascadeDelete: false)
                .ForeignKey("dbo.Ladders", t => t.LadderID, cascadeDelete: false)
                .ForeignKey("dbo.Gamers", t => t.CaptainID)
                .ForeignKey("dbo.MainTeams", t => t.MainTeamID)
                .Index(t => t.SubTeamID)
                .Index(t => t.LeagueID)
                .Index(t => t.LadderID)
                .Index(t => t.CaptainID);
            
            CreateTable(
                "dbo.Gamers",
                c => new
                    {
                        GamerID = c.Int(nullable: false, identity: true),
                        RealUserID = c.String(),
                        XB1Gamertag = c.String(),
                        PSNID = c.String(),
                        DisplayName = c.String(),
                        Active = c.Boolean(nullable: true),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: true),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: true),
                        TwoFactorEnabled = c.Boolean(nullable: true),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: true),
                        AccessFailedCount = c.Int(nullable: true),
                        Id = c.String(),
                        UserName = c.String(),
                        MainTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.GamerID)
                .ForeignKey("dbo.MainTeams", t => t.MainTeamID)
                .Index(t => t.GamerID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Gamer_GamerID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gamers", t => t.Gamer_GamerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Gamer_GamerID);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    PostID = c.Int(nullable: false, identity: true),
                    AuthorID = c.Int(nullable: false),
                    Content = c.String(nullable: false),
                    Date = c.DateTime(nullable: false),
                    LadderID = c.Int(),
                    LeagueID = c.Int(),
                })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Gamers", t => t.AuthorID, cascadeDelete: false)
                .ForeignKey("dbo.Ladders", t => t.LadderID)
                .ForeignKey("dbo.Leagues", t => t.LeagueID)
                .Index(t => t.AuthorID)
                .Index(t => t.LadderID)
                .Index(t => t.LeagueID);
            
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
                        Team1ID = c.Int(nullable: false),
                        Team2ID = c.Int(nullable: false),
                        Result = c.String(nullable: false),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        LeagueID = c.Int(nullable: false),
                        LadderID = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false)
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Ladders", t => t.LadderID, cascadeDelete: false)
                .ForeignKey("dbo.Leagues", t => t.LeagueID, cascadeDelete: false)
                .ForeignKey("dbo.SubTeams", t => t.Team1ID, cascadeDelete: false)
                .ForeignKey("dbo.SubTeams", t => t.Team2ID, cascadeDelete: false)
                .Index(t => t.LeagueID)
                .Index(t => t.LadderID)
                .Index(t => t.Team1ID)
                .Index(t => t.Team2ID);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        LeagueID = c.Int(nullable: false, identity: true),
                        MinPlayers = c.Int(nullable: false),
                        MaxPlayers = c.Int(nullable: false),
                        LeagueName = c.String(nullable: false),
                        GamesPerWeek = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SeasonLength = c.Int(nullable: false),
                        LeagueType = c.String(nullable: false),
                        GameTitle = c.String(nullable: false),
                        Platform = c.String(maxLength: 3),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LeagueID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Gamer_GamerID = c.Int(),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Gamers", t => t.Gamer_GamerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Gamer_GamerID);
            
            CreateTable(
                "dbo.MainTeams",
                c => new
                    {
                        TeamID = c.Int(nullable: false),
                        TeamName = c.String(),
                        DateFounded = c.DateTime(nullable: false),
                        FounderID = c.Int(nullable: false),
                        Website = c.String(),
                        Active = c.Boolean(nullable: false),
                        LogoLink = c.String(),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Gamers", t => t.FounderID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Gamer_GamerID = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Gamers", t => t.Gamer_GamerID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.Gamer_GamerID);
            
            CreateTable(
                "dbo.TeamInvites",
                c => new
                    {
                        TeamInviteID = c.Int(nullable: false, identity: true),
                        MainTeamID = c.Int(nullable: false),
                        SubTeamID = c.Int(nullable: false),
                        InvitedGamerID = c.Int(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        DateAccepted = c.DateTime(nullable: true),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.InvitedGamerID, cascadeDelete: false)
                .ForeignKey("dbo.MainTeams", t => t.MainTeamID, cascadeDelete: false)
                .Index(t => t.TeamInviteID)
                .Index(t => t.SubTeamID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Challenges", "Recipient_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Challenges", "Initiator_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.SubTeams", "SubTeamID", "dbo.MainTeams");
            DropForeignKey("dbo.SubTeams", "Captain_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.TeamInvites", "Team_TeamID", "dbo.MainTeams");
            DropForeignKey("dbo.SubTeams", "TeamInvite_TeamInviteID", "dbo.TeamInvites");
            DropForeignKey("dbo.TeamInvites", "InvitedGamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserRoles", "Gamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.MainTeams", "TeamID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserLogins", "Gamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.Posts", "LeagueID_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "League_LeagueID1", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "LadderID_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Ladder_LadderID1", "dbo.Ladders");
            DropForeignKey("dbo.SubTeams", "LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Matches", "Team2_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Matches", "Team1_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.SubTeams", "LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Challenges", "LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Author_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserClaims", "Gamer_GamerID", "dbo.Gamers");
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TeamInvites", new[] { "Team_TeamID" });
            DropIndex("dbo.TeamInvites", new[] { "InvitedGamer_GamerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.MainTeams", new[] { "TeamID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Matches", new[] { "Team2_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "LadderID" });
            DropIndex("dbo.Matches", new[] { "LeagueID" });
            DropIndex("dbo.Posts", new[] { "LeagueID_LeagueID" });
            DropIndex("dbo.Posts", new[] { "League_LeagueID1" });
            DropIndex("dbo.Posts", new[] { "LadderID_LadderID" });
            DropIndex("dbo.Posts", new[] { "Ladder_LadderID1" });
            DropIndex("dbo.Posts", new[] { "League_LeagueID" });
            DropIndex("dbo.Posts", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "Author_GamerID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.SubTeams", new[] { "Captain_GamerID" });
            DropIndex("dbo.SubTeams", new[] { "TeamInvite_TeamInviteID" });
            DropIndex("dbo.SubTeams", new[] { "LadderID" });
            DropIndex("dbo.SubTeams", new[] { "LeagueID" });
            DropIndex("dbo.SubTeams", new[] { "SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "Recipient_SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "Initiator_SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "LadderID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TeamInvites");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.MainTeams");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Leagues");
            DropTable("dbo.Matches");
            DropTable("dbo.Ladders");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Gamers");
            DropTable("dbo.SubTeams");
            DropTable("dbo.Challenges");
        }
    }
}
