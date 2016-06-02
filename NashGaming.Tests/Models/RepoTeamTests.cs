using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepoTeamTests
    {
        private Mock<DbSet<MainTeam>> _teamSet;
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        [TestMethod]
        private void ConnectMocksToDataStore(IEnumerable<MainTeam> data_store)
        {
            var data_source = data_store.AsQueryable();
            _teamSet.As<IQueryable<MainTeam>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _teamSet.As<IQueryable<MainTeam>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _teamSet.As<IQueryable<MainTeam>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _teamSet.As<IQueryable<MainTeam>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Teams).Returns(_teamSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _repo = new NashGamingRepository(_context.Object);
            _teamSet = new Mock<DbSet<MainTeam>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _repo = null;
            _teamSet = null;
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllTeams()
        {
            List<MainTeam> expected = new List<MainTeam>()
            {
                new MainTeam { TeamID = 1, TeamName="FS1" },
                new MainTeam { TeamID = 2, TeamName="GTXT" }
            };

            _teamSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetAllMainTeams();
            Assert.AreEqual(1, actual[0].TeamID);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepositoryTestsEnsureICanSearchTeamByName()
        {
            List<MainTeam> team_data = new List<MainTeam>
            {
                new MainTeam { TeamName = "Siege the Day" },
                new MainTeam { TeamName = "Day of Siege" },
                new MainTeam { TeamName = "Group Text" }
            };


            _teamSet.Object.AddRange(team_data);
            ConnectMocksToDataStore(team_data);

            List<MainTeam> expected = new List<MainTeam>
            {
                new MainTeam { TeamName = "Day of Siege" },
                new MainTeam { TeamName = "Siege the Day" }
            };

            var actual = _repo.SearchTeamsByName("siege");
            Assert.AreEqual(expected[0].TeamName, actual[0].TeamName);
        }
        [TestMethod]
        public void RepoTeamTestsAddAMainTeam()
        {
            List<MainTeam> db = new List<MainTeam>();
            _teamSet.Object.AddRange(db);
            _teamSet.Setup(o => o.Add(It.IsAny<MainTeam>())).Callback((MainTeam f) => db.Add(f));
            ConnectMocksToDataStore(db);
            MainTeam t = new MainTeam { TeamID = 0 };
            bool result = _repo.AddNewMainTeam(t);
            List<MainTeam> expected = new List<MainTeam> { t };
            List<MainTeam> actual = _repo.GetAllMainTeams();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoTeamTestsRemoveAMainTeam()
        {
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0 }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.InactivateMainTeam(db[0].TeamID);
            List<MainTeam> expected = new List<MainTeam> { new MainTeam { TeamID = 0, Active = false } };
            List<MainTeam> actual = _repo.GetAllMainTeams();
            Assert.IsTrue(result);
            Assert.AreEqual(expected[0].Active, actual[0].Active);
        }

        [TestMethod]
        public void RepoTeamTestsUpdateMainTeamWebsite()
        {
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, Website = "http://.com" }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            string expected = "http://";
            bool result = _repo.UpdateMainTeamWebsite(0, "http://");
            List<MainTeam> actual = _repo.GetAllMainTeams();
            Assert.IsTrue(result);
            Assert.AreEqual(expected, actual[0].Website);
        }
        [TestMethod]
        public void RepoTeamTestsAddSubTeams()
        {
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam>() }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            SubTeam t = new SubTeam { SubTeamID = 0 };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam>() };
            expected.SubTeams.Add(t);
            bool result = _repo.AddSubTeamToMainTeam(0, t);
            List<MainTeam> actual = _repo.GetAllMainTeams();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected.SubTeams, actual[0].SubTeams);
        }
        [TestMethod]
        public void RepoTeamTestsRemoveSubTeams()
        {
            SubTeam t = new SubTeam { SubTeamID = 0 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam>() };
            bool result = _repo.RemoveSubTeamFromMainTeam(0, t);
            List<MainTeam> actual = _repo.GetAllMainTeams();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected.SubTeams, actual[0].SubTeams);
        }
        [TestMethod]
        public void RepoTeamTestsEditSubTeamCaptain()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Captain = new Gamer { GamerID = 0 } };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.UpdateSubTeamCaptain(0, 0, new Gamer { GamerID = 1 });
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Captain = new Gamer { GamerID = 1 } };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Captain, actual.SubTeams[0].Captain);
        }
        [TestMethod]
        public void RepoTeamTestsEditSubTeamRank()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Rank = 1 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.UpdateSubTeamRank(0, 0, 2);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Rank = 2 };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Rank, actual.SubTeams[0].Rank);
        }
        [TestMethod]
        public void RepoTeamTestsEditSubTeamPoints()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Points = 1 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.UpdateSubTeamPoints(0, 0, 2);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Points = 2 };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Points, actual.SubTeams[0].Points);
        }
        [TestMethod]
        public void RepoTeamTestsInactivateSubTeam()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Active = true};
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.InactivateSubTeam(0, 0);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Active = false };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Active, actual.SubTeams[0].Active);
        }
        [TestMethod]
        public void RepoTeamTestsAddSubTeamWins()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Wins = 0 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.AddSubTeamWin(0, 0);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Wins = 1 };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Wins, actual.SubTeams[0].Wins);
        }
        [TestMethod]
        public void RepoTeamTestsAddSubTeamLoss()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Losses = 0 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.AddSubTeamLoss(0, 0);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Losses = 1 };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Losses, actual.SubTeams[0].Losses);
        }
        [TestMethod]
        public void RepoTeamTestsAddGamerToSubTeam()
        {
            SubTeam t = new SubTeam { SubTeamID = 0, Roster = new List<Gamer>() };
            Gamer g = new Gamer { GamerID = 0 };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.AddGamerToSubTeamRoster(0, 0, g);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Roster = new List<Gamer> { g } };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Roster[0], actual.SubTeams[0].Roster[0]);
        }
        [TestMethod]
        public void RepoTeamTestsRemoveGamerFromSubTeam()
        {
            Gamer g = new Gamer { GamerID = 0 };
            SubTeam t = new SubTeam { SubTeamID = 0, Roster = new List<Gamer> { g } };
            List<MainTeam> db = new List<MainTeam>
            {
                new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { t } }
            };
            _teamSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);
            bool result = _repo.RemoveGamerFromSubTeamRoster(0, 0, g);
            SubTeam expectedsub = new SubTeam { SubTeamID = 0, Roster = new List<Gamer>() };
            MainTeam expected = new MainTeam { TeamID = 0, SubTeams = new List<SubTeam> { expectedsub } };
            MainTeam actual = _repo.GetMainTeamByID(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected.SubTeams[0].Roster.Count, actual.SubTeams[0].Roster.Count);
        }
    }
}
