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
    }
}
