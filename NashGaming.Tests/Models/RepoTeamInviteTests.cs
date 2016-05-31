using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Moq;
using System.Linq;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepoTeamInviteTests
    {
        private Mock<DbSet<TeamInvite>> _inviteSet;
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        [TestMethod]
        private void ConnectMocksToDataStore(IEnumerable<TeamInvite> data_store)
        {
            var data_source = data_store.AsQueryable();
            _inviteSet.As<IQueryable<TeamInvite>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _inviteSet.As<IQueryable<TeamInvite>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _inviteSet.As<IQueryable<TeamInvite>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _inviteSet.As<IQueryable<TeamInvite>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Invites).Returns(_inviteSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _repo = new NashGamingRepository(_context.Object);
            _inviteSet = new Mock<DbSet<TeamInvite>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _repo = null;
            _inviteSet = null;
        }
        [TestMethod]
        public void RepoTeamInviteTestsGetTeamInvitesByTeamID()
        {
            MainTeam mt = new MainTeam { TeamID = 0 };
            TeamInvite invite = new TeamInvite { Team = mt, Accepted = true };
            TeamInvite invite2 = new TeamInvite { Team = mt, Accepted = false };
            List<TeamInvite> expected = new List<TeamInvite>
            {
                invite,
                invite2
            };
            List<TeamInvite> inviteDB = new List<TeamInvite>
            {
                invite,
                invite2,
                new TeamInvite { Team = new MainTeam { TeamID = 1 } }
            };
            _inviteSet.Object.AddRange(inviteDB);
            ConnectMocksToDataStore(inviteDB);
            List<TeamInvite> actual = _repo.getInvitesByTeamID(0);
            Assert.AreEqual(2, actual.Count);
            Assert.IsNotNull(_inviteSet.Object);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoTeamInviteTestsGetInvitesByGamerID()
        {
            Gamer g = new Gamer { GamerID = 0 };
            Gamer g2 = new Gamer { GamerID = 1 };
            MainTeam t = new MainTeam { TeamID = 0 };
            List<TeamInvite> db = new List<TeamInvite>
            {
                new TeamInvite { TeamInviteID = 0, Team = t, InvitedGamer = g },
                new TeamInvite { TeamInviteID = 1, Team = t, InvitedGamer = g2 }
            };
            

            _inviteSet.Object.AddRange(db);
            ConnectMocksToDataStore(db);

            List<TeamInvite> expected = new List<TeamInvite>
            {
                new TeamInvite { TeamInviteID = 0, Team = t, InvitedGamer = g }
            };

            List<TeamInvite> actual = _repo.GetTeamInvitesByGamerID(0);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoTeamInviteTestsGetAllInvites()
        {
            List<TeamInvite> expected = new List<TeamInvite> {
                new TeamInvite {TeamInviteID = 0 },
                new TeamInvite {TeamInviteID = 1 }, 
                new TeamInvite {TeamInviteID = 2 }
            };
            _inviteSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            List<TeamInvite> actual = _repo.GetAllTeamInvites();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoTeamInviteDeleteInviteByID()
        {
            List<TeamInvite> InviteDB = new List<TeamInvite>
            {
                new TeamInvite { TeamInviteID = 0 },
                new TeamInvite { TeamInviteID = 1 }
            };
            _inviteSet.Object.AddRange(InviteDB);
            ConnectMocksToDataStore(InviteDB);
            _inviteSet.Setup(o => o.Remove(It.IsAny<TeamInvite>())).Callback((TeamInvite i) => InviteDB.Remove(i));
            bool actual = _repo.DeleteTeamInviteByID(1);
            Assert.IsTrue(actual);
        }
    }
}
