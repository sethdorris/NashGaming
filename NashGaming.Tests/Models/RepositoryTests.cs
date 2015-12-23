using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System.Data.Entity;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        private Mock<DbSet<Gamer>> _gamerSet;
        private Mock<DbSet<NashGaming.Models.Match>> _matchSet;
        private Mock<DbSet<Team>> _teamSet;
        private Mock<DbSet<Gamer>> _playerSet;

        private void ConnectMocksToDataStore(IEnumerable<Gamer> data_store)
        {
            var data_source = data_store.AsQueryable();
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Gamers).Returns(_gamerSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Team> data_store)
        {
            var data_source = data_store.AsQueryable();
            _teamSet.As<IQueryable<Team>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Teams).Returns(_teamSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<NashGaming.Models.Match> data_store)
        {
            var data_source = data_store.AsQueryable();
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Matches).Returns(_matchSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _gamerSet = new Mock<DbSet<Gamer>>();
            _teamSet = new Mock<DbSet<Team>>();
            _matchSet = new Mock<DbSet<NashGaming.Models.Match>>();
            _repo = new NashGamingRepository(_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _gamerSet = null;
            _repo = null;
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanCreateAnInstance()
        {
            NashGamingContext context = _context.Object;
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void RepoEnsureMyRepoIsInitializedOnTestConstruction()
        {
            Assert.IsNotNull(_repo);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllGamers()
        {
            List<Gamer> gamers = new List<Gamer>()
            {
                new Gamer {GamerID = 1, Handle = "Stiffy", Platform = "PS4" },
                new Gamer {GamerID = 2, Handle = "Michael", Platform = "XB1" }
            };

            _gamerSet.Object.AddRange(gamers);
            ConnectMocksToDataStore(gamers);

            var actual = _repo.GetAllGamers();
            Assert.AreEqual(1, actual[0].GamerID);
            CollectionAssert.AreEqual(gamers, actual);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllTeams()
        {
            List<Team> expected = new List<Team>()
            {
                new Team { Rank = 2, TeamID = 1, TeamName="FS1" },
                new Team { Rank = 1, TeamID = 2, TeamName="GTXT" }
            };

            _teamSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetAllTeams();
            Assert.AreEqual(1, actual[0].TeamID);
            CollectionAssert.AreEqual(expected, actual);
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
    }
}
