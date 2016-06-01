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
            Posts p = new Posts { PostID = 0, Content = "Hi" };
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
            Posts p = new Posts { PostID = 0, Content = "Hi" };
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
        public void RepoLadderTestsRemoveSubTeams()
        {
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", Teams = new List<SubTeam>() }
            };
            SubTeam t = new SubTeam { SubTeamID = 0, MainTeam = new MainTeam() };
            l[0].Teams.Add(t);
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.RemoveTeamFromLadder(0, t);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", Teams = new List<SubTeam>() }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsAddSubTeams()
        {
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", Teams = new List<SubTeam>() }
            };
            SubTeam t = new SubTeam { SubTeamID = 0, MainTeam = new MainTeam() };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.AddTeamToLadder(0, t);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", Teams = new List<SubTeam> { t } }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsAddMatches()
        {
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", MaxPlayers = 2, Matches = new List<NashGaming.Models.Match>() }
            };
            NashGaming.Models.Match m = new NashGaming.Models.Match { MatchID = 0, Team1 = new SubTeam(), Team2 = new SubTeam() };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.AddMatchToLadder(0, m);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", MaxPlayers = 2, Matches = new List<NashGaming.Models.Match> { m } }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsRemoveMatches()
        {
            NashGaming.Models.Match m = new NashGaming.Models.Match { MatchID = 0, Team1 = new SubTeam(), Team2 = new SubTeam() };
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", MaxPlayers = 2, Matches = new List<NashGaming.Models.Match> {m } }
            };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.RemoveMatchFromLadder(0, m);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", MaxPlayers = 2, Matches = new List<NashGaming.Models.Match>() }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsRemoveChallenges()
        {
            Challenge c = new Challenge { ChallengeID = 0, Initiator = new SubTeam(), Recipient = new SubTeam() };
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", MaxPlayers = 2, Challenges = new List<Challenge> { c } }
            };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.RemoveChallengeFromLadder(0, c);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", MaxPlayers = 2, Challenges = new List<Challenge>() }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLadderTestsAddChallenges()
        {
            Challenge c = new Challenge { ChallengeID = 0, Initiator = new SubTeam(), Recipient = new SubTeam() };
            List<Ladder> l = new List<Ladder>
            {
                new Ladder {LadderID = 0, GameTitle = "Halo", MaxPlayers = 2, Challenges = new List<Challenge>()}
            };
            _ladderSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.RemoveChallengeFromLadder(0, c);
            List<Ladder> actual = _repo.GetAllLadders();
            List<Ladder> expected = new List<Ladder>
            {
                new Ladder {LadderID =0, GameTitle = "Halo", MaxPlayers = 2, Challenges = new List<Challenge> { c } }
            };
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
