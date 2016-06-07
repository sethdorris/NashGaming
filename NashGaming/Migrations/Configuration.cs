namespace NashGaming.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NashGaming.Models.NashGamingContext>
    {
        public Configuration()
        {

            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NashGaming.Models.NashGamingContext context)
        {
            List<Gamer> Gamerdb = new List<Gamer> {
                        new Gamer { PSNID = "Dude", XB1Gamertag = "N/A", DisplayName = "MockUser1", Email = "MockUser1@Mock.com"},
                        new Gamer { GamerID = 6, PSNID = "Dude2", XB1Gamertag = "N/A", DisplayName = "MockUser2", Email = "MockUser2@Mock.com"},
                        new Gamer { GamerID = 7, PSNID = "Dude3", XB1Gamertag = "N/A", DisplayName = "MockUser3", Email = "MockUser3@Mock.com" },
                        new Gamer { GamerID = 8, PSNID = "Dude4", XB1Gamertag = "N/A", DisplayName = "MockUser4", Email = "MockUser4@Mock.com"},
                        new Gamer { GamerID = 9, PSNID = "Dude5", XB1Gamertag = "N/A", DisplayName = "MockUser5", Email = "MockUser5@Mock.com"},
                        new Gamer { GamerID = 10, PSNID = "Dude6", XB1Gamertag = "N/A", DisplayName = "MockUser6", Email = "MockUser6@Mock.com"},
                        new Gamer { GamerID = 11, PSNID = "Dude7", XB1Gamertag = "N/A", DisplayName = "MockUser7", Email = "MockUser7@Mock.com"},
                        new Gamer { GamerID = 12, PSNID = "Dude8", XB1Gamertag = "N/A", DisplayName = "MockUser8", Email = "MockUser8@Mock.com"},
                        new Gamer { GamerID = 13, PSNID = "Dude9", XB1Gamertag = "N/A", DisplayName = "MockUser9", Email = "MockUser9@Mock.com"},
                        new Gamer { GamerID = 14, PSNID = "Dude10", XB1Gamertag = "N/A", DisplayName = "LadderTeam1Guy1", Email = "MockUser10@Mock.com"},
                        new Gamer { GamerID = 15, PSNID = "Dude11", XB1Gamertag = "N/A", DisplayName = "LadderTeam1Guy2", Email = "MockUser11@Mock.com"},
                        new Gamer { GamerID = 16, PSNID = "Dude12", XB1Gamertag = "N/A", DisplayName = "LadderTeam1Guy3", Email = "MockUser12@Mock.com"},
                        new Gamer { GamerID = 17, PSNID = "Dude13", XB1Gamertag = "N/A", DisplayName = "LadderTeam1Guy4", Email = "MockUser13@Mock.com"},
                        new Gamer { GamerID = 18, PSNID = "Dude14", XB1Gamertag = "N/A", DisplayName = "LadderTeam2Guy1", Email = "MockUser14@Mock.com"},
                        new Gamer { GamerID = 19, PSNID = "Dude15", XB1Gamertag = "N/A", DisplayName = "LadderTeam2Guy2", Email = "MockUser15@Mock.com"},
                        new Gamer { GamerID = 20, PSNID = "Dude16", XB1Gamertag = "N/A", DisplayName = "LadderTeam2Guy3", Email = "MockUser16@Mock.com"},
                        new Gamer { GamerID = 21, PSNID = "Dude17", XB1Gamertag = "N/A", DisplayName = "LadderTeam2Guy4", Email = "MockUser17@Mock.com"},
                        new Gamer { GamerID = 22, PSNID = "Dude18", XB1Gamertag = "N/A", DisplayName = "RandomGamer1", Email = "MockUser18@Mck.com"},
                        new Gamer { GamerID = 23, PSNID = "Dude19", XB1Gamertag = "N/A", DisplayName = "RandomGamer2", Email = "MockUser19@Mock.com"}
                    };
            List<MainTeam> MainTeamdb = new List<MainTeam>
                {
                    new MainTeam { MainTeamID =0, DateFounded = new DateTime(2016, 06, 01), TeamName = "MockTeam1", Website = "https://team1.com", Active = true },
                    new MainTeam { MainTeamID =1, DateFounded = new DateTime(2016, 06, 01), TeamName = "MockTeam2", Website = "https://team2.com", Active = true },
                    new MainTeam { MainTeamID =2, DateFounded = new DateTime(2016, 06, 01), TeamName = "MockTeam3", Website = "https://team3.com", Active = true },
                };
            List<League> Leaguedb = new List<League>
                {
                    new League { LeagueID = 0, GamesPerWeek = 1, GameTitle = "CSGO", Platform = "PC", EndDate = new DateTime(2016, 09, 01), StartDate = new DateTime(2016, 06, 01), LeagueName = "CSGO 4v4 OBJ League", MinPlayers = 2, MaxPlayers = 4, LeagueType = "Open", Active = true, MainTeams = null, Matches = new List<Match>(), Feed = new List<Posts>() },
                    new League { LeagueID = 1, GamesPerWeek = 2, GameTitle = "Halo", Platform = "XB1", EndDate = new DateTime(2016, 09, 01), StartDate = new DateTime(2016, 06, 01), LeagueName = "Halo 4v4 TDM League", MinPlayers = 2, MaxPlayers = 4, LeagueType = "Intermediate", Active = true, MainTeams = null, Matches = new List<Match>(), Feed = new List<Posts>() },
                    new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "Rainbow Six: Siege", Platform = "PS4", EndDate = new DateTime(2016, 09, 01), StartDate = new DateTime(2016, 06, 01), LeagueName = "Rainbow6 Siege 5v5 League", MinPlayers = 3, MaxPlayers = 5, LeagueType = "Invite", Active = true, MainTeams = null, Matches = new List<Match>(), Feed = new List<Posts>() }
                };
            int l1seasonlength = Convert.ToInt16((Leaguedb[0].EndDate - Leaguedb[0].StartDate).Days / 7);
            int l2seasonlength = Convert.ToInt16((Leaguedb[1].EndDate - Leaguedb[1].StartDate).Days / 7);
            int l3seasonlength = Convert.ToInt16((Leaguedb[2].EndDate - Leaguedb[2].StartDate).Days / 7);
            Leaguedb[0].SeasonLength = l1seasonlength;
            Leaguedb[1].SeasonLength = l2seasonlength;
            Leaguedb[2].SeasonLength = l3seasonlength;
            List<Ladder> Ladderdb = new List<Ladder>
                {
                    new Ladder { LadderID = 0, GameTitle = "CSGO", Platform = "PC", LadderName = "2v2 Pistols Only", MinPlayers = 1, MaxPlayers = 2, Active = true, MainTeams = null, Challenges = new List<Challenge>(), Feed = new List<Posts>(), Matches = new List<Match>() }
                };
            
            TeamInvite ti1 = new TeamInvite { TeamInviteID = 0, InvitedGamer = Gamerdb[8], Team = MainTeamdb[0], Accepted = false, DateSent = new DateTime(2016, 06, 03) };
            List<TeamInvite> TeamInviteDB = new List<TeamInvite> { ti1 };
            //Challenge challenge1 = new Challenge { ChallengeID = 0, Accepted = false, Ladder = Ladderdb[0], Initiator = ladder1team1, Recipient = ladder1team2, ProposedDate1 = new DateTime(2016, 06, 09, 18, 00, 00), ProposedDate2 = new DateTime(2016, 06, 12, 17, 15, 00), ProposedDate3 = new DateTime(2016, 06, 15, 21, 30, 00) };
            //List<Challenge> cdb = new List<Challenge> { challenge1 };
            //Ladderdb[0].Challenges.Add(challenge1);
            //context.Challenges.AddRange(cdb);
            context.Gamers.AddRange(Gamerdb);
            //context.Invites.AddRange(TeamInviteDB);
            context.Teams.AddRange(MainTeamdb);
            context.Ladders.AddRange(Ladderdb);
            context.Leagues.AddRange(Leaguedb);
            context.SaveChanges();
        }
    }
}
