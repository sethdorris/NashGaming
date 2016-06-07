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
    public class RepoLadderTests
    {
        private Mock<DbSet<Ladder>> _ladderSet;
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        [TestMethod]
        private void ConnectMocksToDataStore(IEnumerable<Ladder> data_store)
        {
            var data_source = data_store.AsQueryable();
            _ladderSet.As<IQueryable<Ladder>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _ladderSet.As<IQueryable<Ladder>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _ladderSet.As<IQueryable<Ladder>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _ladderSet.As<IQueryable<Ladder>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Ladders).Returns(_ladderSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _repo = new NashGamingRepository(_context.Object);
            _ladderSet = new Mock<DbSet<Ladder>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _repo = null;
            _ladderSet = null;
        }

        [TestMethod]
        public void RepoLadderTestsGetLadderByGameTitle()
        {
            List<Ladder> ladderdb = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2 },
                new Ladder { LadderID = 1, GameTitle = "Halo", MinPlayers = 4 },
                new Ladder { LadderID = 2, GameTitle = "COD", MinPlayers = 2 }
            };

            List<Ladder> expected = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2 },
                new Ladder { LadderID = 1, GameTitle = "Halo", MinPlayers = 4 }
            };

            _ladderSet.Object.AddRange(ladderdb);
            ConnectMocksToDataStore(ladderdb);
            List<Ladder> actual = _repo.GetLaddersByGameTitle("Halo");
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsAddLadder()
        {
            List<Ladder> ladderdb = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2 },
            };

            List<Ladder> expected = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2 },
                new Ladder { LadderID = 1, GameTitle = "Halo", MinPlayers = 4 }
            };
            Ladder l = new Ladder { LadderID = 1, GameTitle = "Halo", MinPlayers = 4 };
            _ladderSet.Object.AddRange(ladderdb);
            _ladderSet.Setup(o => o.Add(It.IsAny<Ladder>())).Callback((Ladder d) => ladderdb.Add(d));
            ConnectMocksToDataStore(ladderdb);
            bool result = _repo.AddLadder(l);
            List<Ladder> actual = _repo.GetAllLadders();
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsAddPostToLadder()
        {
            List<Ladder> ladderdb = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2, Feed = new List<Posts>() },
            };
            Posts p = new Posts { PostsID = 0, Content = "Hi" };
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2, Feed = new List<Posts> { p } },
            };
            _ladderSet.Object.AddRange(ladderdb);
            ConnectMocksToDataStore(ladderdb);
            bool result = _repo.AddPostToLadder(0, p);
            List<Ladder> actual = _repo.GetAllLadders();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsRemovePostFromLadder()
        {
            Posts p = new Posts { PostsID = 0, Content = "Hi" };
            List<Ladder> ladderdb = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2, Feed = new List<Posts> { p } },
            };
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder { LadderID = 0, GameTitle = "Halo", MinPlayers = 2, Feed = new List<Posts>() },
            };
            _ladderSet.Object.AddRange(ladderdb);
            ConnectMocksToDataStore(ladderdb);
            bool result = _repo.RemovePostFromLadder(0, p);
            List<Ladder> actual = _repo.GetAllLadders();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsInactivateALadder()
        {
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, Active = true }
            };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID = 0, Active = false }
            };
            bool result = _repo.InactivateLadder(0);
            List<Ladder> actual = _repo.GetAllLadders();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
