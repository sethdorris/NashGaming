namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Initiator_SubTeamID = c.Int(),
                        Ladder_LadderID = c.Int(),
                        Recipient_SubTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.ChallengeID)
                .ForeignKey("dbo.SubTeams", t => t.Initiator_SubTeamID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .ForeignKey("dbo.SubTeams", t => t.Recipient_SubTeamID)
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
                        MainTeam_TeamID = c.Int(),
                        TeamInvite_TeamInviteID = c.Int(),
                        Captain_GamerID = c.Int(),
                        League_LeagueID = c.Int(),
                        Ladder_LadderID = c.Int(),
                    })
                .PrimaryKey(t => t.SubTeamID)
                .ForeignKey("dbo.MainTeams", t => t.MainTeam_TeamID)
                .ForeignKey("dbo.TeamInvites", t => t.TeamInvite_TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.Captain_GamerID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .Index(t => t.MainTeam_TeamID)
                .Index(t => t.TeamInvite_TeamInviteID)
                .Index(t => t.Captain_GamerID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Ladder_LadderID);
            
            CreateTable(
                "dbo.Gamers",
                c => new
                    {
                        GamerID = c.Int(nullable: false, identity: true),
                        RealUserID = c.String(),
                        XB1Gamertag = c.String(),
                        PSNID = c.String(),
                        Active = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Id = c.String(),
                        UserName = c.String(),
                        SubTeam_SubTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.GamerID)
                .ForeignKey("dbo.SubTeams", t => t.SubTeam_SubTeamID)
                .Index(t => t.SubTeam_SubTeamID);
            
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
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        LeagueID = c.Int(nullable: false),
                        Author_GamerID = c.Int(),
                        Ladder_LadderID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Gamers", t => t.Author_GamerID)
                .ForeignKey("dbo.Leagues", t => t.LeagueID, cascadeDelete: true)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .Index(t => t.LeagueID)
                .Index(t => t.Author_GamerID)
                .Index(t => t.Ladder_LadderID);
            
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
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false),
                        DateFounded = c.DateTime(nullable: false),
                        Website = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Gamers", t => t.TeamID)
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
                        DateSent = c.DateTime(nullable: false),
                        DateAccepted = c.DateTime(),
                        Accepted = c.Boolean(nullable: false),
                        InvitedGamer_GamerID = c.Int(),
                        Team_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.TeamInviteID)
                .ForeignKey("dbo.Gamers", t => t.InvitedGamer_GamerID)
                .ForeignKey("dbo.MainTeams", t => t.Team_TeamID)
                .Index(t => t.InvitedGamer_GamerID)
                .Index(t => t.Team_TeamID);
            
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
                        LeagueType = c.String(),
                        GameTitle = c.String(nullable: false),
                        Platform = c.String(maxLength: 3),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LeagueID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        DatePlayed = c.DateTime(),
                        Result = c.String(),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        League_LeagueID = c.Int(),
                        Team1_SubTeamID = c.Int(nullable: false),
                        Team2_SubTeamID = c.Int(nullable: false),
                        Ladder_LadderID = c.Int(),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Leagues", t => t.League_LeagueID)
                .ForeignKey("dbo.SubTeams", t => t.Team1_SubTeamID, cascadeDelete: false)
                .ForeignKey("dbo.SubTeams", t => t.Team2_SubTeamID, cascadeDelete: false)
                .ForeignKey("dbo.Ladders", t => t.Ladder_LadderID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Team1_SubTeamID)
                .Index(t => t.Team2_SubTeamID)
                .Index(t => t.Ladder_LadderID);
            
            CreateTable(
                "dbo.Ladders",
                c => new
                    {
                        LadderID = c.Int(nullable: false, identity: true),
                        LadderName = c.String(nullable: false),
                        GameTitle = c.String(nullable: false),
                        MinPlayers = c.Int(nullable: false),
                        MaxPlayers = c.Int(nullable: false),
                        Platform = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LadderID);
            
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
            DropForeignKey("dbo.SubTeams", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Matches", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Posts", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Challenges", "Ladder_LadderID", "dbo.Ladders");
            DropForeignKey("dbo.Challenges", "Initiator_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Gamers", "SubTeam_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.SubTeams", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "Team2_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Matches", "Team1_SubTeamID", "dbo.SubTeams");
            DropForeignKey("dbo.Matches", "League_LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.Posts", "LeagueID", "dbo.Leagues");
            DropForeignKey("dbo.SubTeams", "Captain_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.TeamInvites", "Team_TeamID", "dbo.MainTeams");
            DropForeignKey("dbo.SubTeams", "TeamInvite_TeamInviteID", "dbo.TeamInvites");
            DropForeignKey("dbo.TeamInvites", "InvitedGamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserRoles", "Gamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.SubTeams", "MainTeam_TeamID", "dbo.MainTeams");
            DropForeignKey("dbo.MainTeams", "TeamID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserLogins", "Gamer_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.Posts", "Author_GamerID", "dbo.Gamers");
            DropForeignKey("dbo.AspNetUserClaims", "Gamer_GamerID", "dbo.Gamers");
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Matches", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Matches", new[] { "Team2_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "Team1_SubTeamID" });
            DropIndex("dbo.Matches", new[] { "League_LeagueID" });
            DropIndex("dbo.TeamInvites", new[] { "Team_TeamID" });
            DropIndex("dbo.TeamInvites", new[] { "InvitedGamer_GamerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.MainTeams", new[] { "TeamID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Posts", new[] { "Author_GamerID" });
            DropIndex("dbo.Posts", new[] { "LeagueID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Gamer_GamerID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Gamers", new[] { "SubTeam_SubTeamID" });
            DropIndex("dbo.SubTeams", new[] { "Ladder_LadderID" });
            DropIndex("dbo.SubTeams", new[] { "League_LeagueID" });
            DropIndex("dbo.SubTeams", new[] { "Captain_GamerID" });
            DropIndex("dbo.SubTeams", new[] { "TeamInvite_TeamInviteID" });
            DropIndex("dbo.SubTeams", new[] { "MainTeam_TeamID" });
            DropIndex("dbo.Challenges", new[] { "Recipient_SubTeamID" });
            DropIndex("dbo.Challenges", new[] { "Ladder_LadderID" });
            DropIndex("dbo.Challenges", new[] { "Initiator_SubTeamID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ladders");
            DropTable("dbo.Matches");
            DropTable("dbo.Leagues");
            DropTable("dbo.TeamInvites");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.MainTeams");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Gamers");
            DropTable("dbo.SubTeams");
            DropTable("dbo.Challenges");
        }
    }
}
