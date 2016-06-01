using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System.Data.Entity;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepoChallengeTests
    {
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        private Mock<DbSet<Challenge>> _challengeSet;

        private void ConnectMocksToDataStore(IEnumerable<Challenge> data_store)
        {
            var data_source = data_store.AsQueryable();
            _challengeSet.As<IQueryable<Challenge>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _challengeSet.As<IQueryable<Challenge>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _challengeSet.As<IQueryable<Challenge>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _challengeSet.As<IQueryable<Challenge>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Challenges).Returns(_challengeSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _challengeSet = new Mock<DbSet<Challenge>>();
            _repo = new NashGamingRepository(_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _challengeSet = null;
            _repo = null;
        }
        [TestMethod]
        public void RepoChallengeTestsGetChallengeByLadderID()
        {
            Ladder ld1 = new Ladder { LadderID = 0 };
            Ladder ld2 = new Ladder { LadderID = 1 };
            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1 },
                new Challenge { ChallengeID = 1, Ladder = ld1 },
                new Challenge { ChallengeID = 2, Ladder = ld2 }
            };
            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);
            List<Challenge> expected = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1 },
                new Challenge { ChallengeID = 1, Ladder = ld1 }
            };
            List<Challenge> actual = _repo.GetChallengesByLadderID(0);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoChallengeTestsGetChallengeByChallengedTeamID()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };

            Ladder ld1 = new Ladder { LadderID = 0 };
            Ladder ld2 = new Ladder { LadderID = 1 };
            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 },
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 },
                new Challenge { ChallengeID = 2, Ladder = ld2, Initiator = mtst2, Recipient = mtst3 }
            };
            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);
            List<Challenge> expected = new List<Challenge>
            {
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 },
                new Challenge { ChallengeID = 2, Ladder = ld2, Initiator = mtst2, Recipient = mtst3 }
            };
            List<Challenge> actual = _repo.GetChallengesByRecipientTeamID(2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoChallengeTestsGetChallengeByChallengingTeamID()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };

            Ladder ld1 = new Ladder { LadderID = 0 };
            Ladder ld2 = new Ladder { LadderID = 1 };
            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 },
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 },
                new Challenge { ChallengeID = 2, Ladder = ld2, Initiator = mtst2, Recipient = mtst3 }
            };
            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);
            List<Challenge> expected = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 },
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 }
            };
            List<Challenge> actual = _repo.GetChallengesByInitiatorTeamID(0);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
