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
    }
}

        
      

        
