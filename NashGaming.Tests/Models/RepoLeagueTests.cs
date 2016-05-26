using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NashGaming.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepoLeagueTests
    {
        private Mock<DbSet<League>> _leagueSet;
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        [TestMethod]
        private void ConnectMocksToDataStore(IEnumerable<League> data_store)
        {
            var data_source = data_store.AsQueryable();
            _leagueSet.As<IQueryable<League>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Leagues).Returns(_leagueSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _repo = new NashGamingRepository(_context.Object);
            _leagueSet = new Mock<DbSet<League>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _repo = null;
            _leagueSet = null;
        }

        [TestMethod]
        public void RepoEnsureICanGetAllLeagues()
        {
            List<League> expected = new List<League>
            {
                new League { LeagueName = "COD"},
                new League { LeagueName = "Halo" }
            };


            _leagueSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            List<League> actual = _repo.GetAllLeagues();
            Assert.IsNotNull(expected);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
