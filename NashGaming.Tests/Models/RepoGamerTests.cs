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
                new Gamer {GamerID = 1},
                new Gamer {GamerID = 2}
            };

            _gamerSet.Object.AddRange(gamers);
            ConnectMocksToDataStore(gamers);

            var actual = _repo.GetAllGamers();
            Assert.AreEqual(1, actual[0].GamerID);
            CollectionAssert.AreEqual(gamers, actual);
        }

        //[TestMethod]
        //public void RepositoryTestsEnsureICanGetGamerByHandle()
        //{
        //    List<Gamer> expected = new List<Gamer>
        //    {
        //        new Gamer { GamerID = 1, Username = "StiffNasty" },
        //        new Gamer { GamerID = 2, Username = "Bro" },
        //        new Gamer { GamerID = 3, Username = "StiffNasty45" }
        //    };
            

        //    _gamerSet.Object.AddRange(expected);
        //    ConnectMocksToDataStore(expected);

        //    var actual = _repo.GetGamerByHandle("StiffNasty");
        //    Assert.AreEqual(2, actual.Count);
        //    Assert.AreEqual(1, actual[0].GamerID);
        //    Assert.AreEqual(3, actual[1].GamerID);
        //}

        [TestMethod]
        public void RepoEnsureICanAddGamerToDB()
        {
            List<Gamer> expected = new List<Gamer> { new Gamer { GamerID = 0} };
            Gamer me = new Gamer { GamerID = 1};

            _gamerSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            _gamerSet.Setup(o => o.Add(It.IsAny<Gamer>())).Callback((Gamer g) => expected.Add(g));

            bool actual = _repo.AddGamer(me);
            Assert.IsTrue(actual);
            Assert.AreEqual(2, expected.Count);
        }

        [TestMethod]
        public void RepoGamerTestsDeleteGamerByID()
        {
            List<Gamer> gamerdb = new List<Gamer>
            {
                new Gamer { GamerID = 0, Active = true, UserName = "Steve" },
                new Gamer { GamerID = 1, Active = true, UserName = "Jon" }
            };
            _gamerSet.Object.AddRange(gamerdb);
            ConnectMocksToDataStore(gamerdb);
            Gamer expected = new Gamer { GamerID = 0, Active = false, UserName = "Steve" };
            bool testcall = _repo.DeleteGamerById(0);
            Gamer actual = _repo.GetGamerById(0);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testcall);
        }
        [TestMethod]
        public void RepoGamerTestsUpdateGamerMT()
        {
            List<Gamer> gamerdb = new List<Gamer>
            {
                new Gamer { GamerID = 0, Active = true, UserName = "Steve" },
                new Gamer { GamerID = 1, Active = true, UserName = "Jon" }
            };
            MainTeam t = new MainTeam { TeamID = 0, TeamName = "stk" };
            _gamerSet.Object.AddRange(gamerdb);
            ConnectMocksToDataStore(gamerdb);
            Gamer expected = new Gamer { GamerID = 0, Active = false, UserName = "Steve", MainTeam = t };
            bool testcall = _repo.UpdateGamerMainTeam(0, t);
            Gamer actual = _repo.GetGamerById(0);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(testcall);
        }
        [TestMethod]
        public void RepoGamerTestsUpdateGamerComments()
        {
            List<Gamer> gamerdb = new List<Gamer>
            {
                new Gamer { GamerID = 0, Active = true, UserName = "Steve", Comments = new List<Posts>() },
                new Gamer { GamerID = 1, Active = true, UserName = "Jon" }
            };
            Posts p = new Posts { PostID = 0, Content = "Hi" };

            _gamerSet.Object.AddRange(gamerdb);
            ConnectMocksToDataStore(gamerdb);
            Gamer expected = new Gamer { GamerID = 0, Active = true, UserName = "Steve", Comments = new List<Posts>() };
            expected.Comments.Add(p);
            bool result = _repo.UpdateGamerComments(0, p);
            Gamer actual = _repo.GetGamerById(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoGamerTestsUpdateGamerInvites()
        {
            List<Gamer> gamerdb = new List<Gamer>
            {
                new Gamer { GamerID = 0, Active = true, UserName = "Steve", TeamInvites = new List<TeamInvite>() },
                new Gamer { GamerID = 1, Active = true, UserName = "Jon" }
            };
            TeamInvite i = new TeamInvite { TeamInviteID = 1, Team = new MainTeam() };

            _gamerSet.Object.AddRange(gamerdb);
            ConnectMocksToDataStore(gamerdb);
            Gamer expected = new Gamer { GamerID = 0, Active = true, UserName = "Steve", TeamInvites = new List<TeamInvite>() };
            expected.TeamInvites.Add(i);
            bool result = _repo.UpdateGamerTeamInvites(0, i);
            Gamer actual = _repo.GetGamerById(0);
            Assert.IsTrue(result);
            Assert.AreEqual(expected, actual);
        }
    }
}
