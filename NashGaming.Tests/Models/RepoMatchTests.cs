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
    }
}
