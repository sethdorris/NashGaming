using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using NashGaming.Models;
using System.Collections.Generic;
using System.Linq;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepoMatchTests
    {
        private Mock<DbSet<NashGaming.Models.Match>> _matchSet;
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        [TestMethod]
        private void ConnectMocksToDataStore(IEnumerable<NashGaming.Models.Match> data_store)
        {
            var data_source = data_store.AsQueryable();
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Matches).Returns(_matchSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _repo = new NashGamingRepository(_context.Object);
            _matchSet = new Mock<DbSet<NashGaming.Models.Match>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _repo = null;
            _matchSet = null;
        }
        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllMatches()
        {
            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>()
            {
                new NashGaming.Models.Match { MatchID = 1, Result = "WIN", Team1Score = 3, Team2Score = 2 },
                new NashGaming.Models.Match { MatchID = 2, Result = "LOSS", Team1Score = 2, Team2Score = 3 }
            };

            _matchSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetAllMatches();
            Assert.AreEqual(1, actual[0].MatchID);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoMatchTestsGetMatchesByMainTeamID()
        {
            MainTeam MT1 = new MainTeam { TeamID = 0, Active = true };
            MainTeam MT2 = new MainTeam { TeamID = 1, Active = true };
            SubTeam ST1 = new SubTeam { MainTeam = MT1, SubTeamName = "ST1" };
            SubTeam ST2 = new SubTeam { MainTeam = MT1, SubTeamName = "ST2" };
            SubTeam ST3 = new SubTeam { MainTeam = MT2, SubTeamName = "ST3" };
            SubTeam ST4 = new SubTeam { MainTeam = MT2, SubTeamName = "ST4" };
            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { Team1 = ST1, Team2 = ST3, League = new League { GameTitle = "Halo"} },
                new NashGaming.Models.Match { Team1 = ST2, Team2 = ST4, League = new League { GameTitle = "Halo"} },
                new NashGaming.Models.Match { Team1 = ST1, Team2 = ST4, League = new League { GameTitle = "COD"} }
            };
            _matchSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            List<NashGaming.Models.Match> actual = _repo.GetMatchesByTeamID(0);
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(expected[2], actual[0]);
        }

        [TestMethod]
        public void RepoMatchTestsGetMatchesByMainTeamName()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "mt3" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };

            List<NashGaming.Models.Match> MatchDB = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3 },
                new NashGaming.Models.Match { MatchID = 2, Team1 = mtst2, Team2 = mtst3 }
            };

            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3 }
            };
            _matchSet.Object.AddRange(MatchDB);
            ConnectMocksToDataStore(MatchDB);
            List<NashGaming.Models.Match> actual = _repo.GetMatchesByTeamName(mt1.TeamName);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoMatchTestsGetMatchesByDatePlayed()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "mt3" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };

            List<NashGaming.Models.Match> MatchDB = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01) },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 03) },
                new NashGaming.Models.Match { MatchID = 2, Team1 = mtst2, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 05) }
            };

            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01) },
            };
            _matchSet.Object.AddRange(MatchDB);
            ConnectMocksToDataStore(MatchDB);
            List<NashGaming.Models.Match> actual = _repo.GetMatchesByDatePlayed(new DateTime(2016, 05, 01));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoMatchTestsGetMatchesByLeagueID()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "mt3" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> MatchDB = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 03), League = l1 },
                new NashGaming.Models.Match { MatchID = 2, Team1 = mtst2, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 05), League = l2 }
            };

            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 03), League = l1 }
            };
            _matchSet.Object.AddRange(MatchDB);
            ConnectMocksToDataStore(MatchDB);
            List<NashGaming.Models.Match> actual = _repo.GetMatchesByLeagueID(0);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoMatchTestsGetMatchesByGameTitle()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "mt3" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> MatchDB = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 03), League = l1 },
                new NashGaming.Models.Match { MatchID = 2, Team1 = mtst2, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 05), League = l2 }
            };

            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst1, Team2 = mtst3, DatePlayed = new DateTime(2016, 05, 03), League = l1 }
            };
            _matchSet.Object.AddRange(MatchDB);
            ConnectMocksToDataStore(MatchDB);
            List<NashGaming.Models.Match> actual = _repo.GetMatchesByGameTitle("Halo");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanAddMatchToDB()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };

            List<NashGaming.Models.Match> db = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
            };
            NashGaming.Models.Match testMatch = new NashGaming.Models.Match { MatchID = 1 };

            _matchSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            _matchSet.Setup(o => o.Add(It.IsAny<NashGaming.Models.Match>())).Callback((NashGaming.Models.Match g) => db.Add(g));

            bool actual = _repo.AddMatch(testMatch);
            List<NashGaming.Models.Match> adb = _repo.GetAllMatches();
            List<NashGaming.Models.Match> expected = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                testMatch
            };

            Assert.IsTrue(actual);
            CollectionAssert.AreEqual(expected, adb);
        }

        [TestMethod]
        public void RepoMatchTestsUpdateMatchDatePlayed()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> matchdb = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 1), League = l1 }
            };

            _matchSet.Object.AddRange(matchdb);
            ConnectMocksToDataStore(matchdb);

            NashGaming.Models.Match expected = new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 15) };
            bool result = _repo.UpdateMatchDatePlayed(0, new DateTime(2016, 05, 15));
            NashGaming.Models.Match actual = _repo.GetMatchById(0);

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoMatchTestsUpdateMatchResult()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> matchdb = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Result = "LOSS", League = l1 }
            };

            _matchSet.Object.AddRange(matchdb);
            ConnectMocksToDataStore(matchdb);

            NashGaming.Models.Match expected = new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Result = "WIN"};
            bool result = _repo.UpdateMatchResult(0, "WIN");
            NashGaming.Models.Match actual = _repo.GetMatchById(0);

            Assert.AreEqual(expected.Result, actual.Result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoMatchTestsUpdateMatchScore1()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> matchdb = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Team1Score = 50, League = l1 }
            };

            _matchSet.Object.AddRange(matchdb);
            ConnectMocksToDataStore(matchdb);

            NashGaming.Models.Match expected = new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Team1Score = 63 };
            bool result = _repo.UpdateMatchTeam1Score(0, 63);
            NashGaming.Models.Match actual = _repo.GetMatchById(0);

            Assert.AreEqual(expected.Team1Score, actual.Team1Score);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoMatchTestsUpdateMatchScore2()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> matchdb = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Team2Score = 60, League = l1 }
            };

            _matchSet.Object.AddRange(matchdb);
            ConnectMocksToDataStore(matchdb);

            NashGaming.Models.Match expected = new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, Team2Score = 73 };
            bool result = _repo.UpdateMatchTeam2Score(0, 73);
            NashGaming.Models.Match actual = _repo.GetMatchById(0);

            Assert.AreEqual(expected.Team2Score, actual.Team2Score);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoMatchTestsUpdateMatchCompleted()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "StK" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DD" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            League l1 = new League { LeagueID = 0, GameTitle = "Halo" };
            League l2 = new League { LeagueID = 1, GameTitle = "CSGO" };

            List<NashGaming.Models.Match> matchdb = new List<NashGaming.Models.Match>
            {
                new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 01), League = l1 },
                new NashGaming.Models.Match { MatchID = 1, Team1 = mtst2, Team2 = mtst1, DatePlayed = new DateTime(2016, 06, 01), League = l2 },
                new NashGaming.Models.Match { MatchID = 2, Team1 = mtst1, Team2 = mtst2, DatePlayed = new DateTime(2016, 05, 31), League = l1 },
            };

            _matchSet.Object.AddRange(matchdb);
            ConnectMocksToDataStore(matchdb);

            NashGaming.Models.Match expected = new NashGaming.Models.Match { MatchID = 0, Team1 = mtst1, Team2 = mtst2, League = l1, Completed = true };
            bool result = _repo.UpdateMatchCompletion(0, true);
            NashGaming.Models.Match actual = _repo.GetMatchById(0);

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(result);
        }
    }
}
