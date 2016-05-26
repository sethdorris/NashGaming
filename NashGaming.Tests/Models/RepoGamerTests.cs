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
    public class RepoGamerTests
    {
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        private Mock<DbSet<Gamer>> _gamerSet;

        private void ConnectMocksToDataStore(IEnumerable<Gamer> data_store)
        {
            var data_source = data_store.AsQueryable();
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Gamers).Returns(_gamerSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _gamerSet = new Mock<DbSet<Gamer>>();
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
                new Gamer {GamerID = 1, Handle = "Stiffy"},
                new Gamer {GamerID = 2, Handle = "Michael"}
            };

            _gamerSet.Object.AddRange(gamers);
            ConnectMocksToDataStore(gamers);

            var actual = _repo.GetAllGamers();
            Assert.AreEqual(1, actual[0].GamerID);
            CollectionAssert.AreEqual(gamers, actual);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetGamerByHandle()
        {
            List<Gamer> expected = new List<Gamer>
            {
                new Gamer { GamerID = 1, Handle = "StiffNasty" },
                new Gamer { GamerID = 2, Handle = "Bro" },
                new Gamer { GamerID = 3, Handle = "StiffNasty45" }
            };
            

            _gamerSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetGamerByHandle("StiffNasty");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].GamerID);
            Assert.AreEqual(3, actual[1].GamerID);
        }

        [TestMethod]
        public void RepoEnsureICanAddGamerToDB()
        {
            List<Gamer> expected = new List<Gamer> { new Gamer { Handle = "StiffNasty" } };
            Gamer me = new Gamer { Handle = "Plah"};

            _gamerSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            _gamerSet.Setup(o => o.Add(It.IsAny<Gamer>())).Callback((Gamer g) => expected.Add(g));

            bool actual = _repo.AddGamer(me);
            Assert.IsTrue(actual);
            Assert.AreEqual(2, expected.Count);
        }
    }
}
