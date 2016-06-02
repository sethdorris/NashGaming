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

        [TestMethod]
        public void RepoEnsureICanAddChallengeToDB()
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
            Challenge testChallenge = new Challenge { ChallengeID = 3 };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);
            _challengeSet.Setup(o => o.Add(It.IsAny<Challenge>())).Callback((Challenge c) => cdb.Add(c));

            bool actual = _repo.AddChallenge(testChallenge);
            List<Challenge> acdb = _repo.GetAllChallenges(); 
            List<Challenge> expected = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 },
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 },
                new Challenge { ChallengeID = 2, Ladder = ld2, Initiator = mtst2, Recipient = mtst3 },
                testChallenge
            };

            Assert.IsTrue(actual);
            CollectionAssert.AreEqual(expected, acdb);
        }

        [TestMethod]
        public void RepoChallengeTestsUpdateChallengeProposedDate1()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            Ladder ld1 = new Ladder { LadderID = 0 };

            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, ProposedDate1 = new DateTime(2016, 05, 05), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 }
            };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);

            Challenge expected = new Challenge { ChallengeID = 0, ProposedDate1 = new DateTime(2016, 05, 15), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 };
            bool result = _repo.UpdateChallengeProposedDate1(0, new DateTime(2016, 05, 15));
            Challenge actual = _repo.GetChallengeById(0);

            Assert.AreEqual(expected.ProposedDate1, actual.ProposedDate1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoChallengeTestsUpdateChallengeProposedDate2()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            Ladder ld1 = new Ladder { LadderID = 0 };

            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, ProposedDate2 = new DateTime(2016, 05, 05), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 }
            };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);

            Challenge expected = new Challenge { ChallengeID = 0, ProposedDate2 = new DateTime(2016, 05, 15), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 };
            bool result = _repo.UpdateChallengeProposedDate2(0, new DateTime(2016, 05, 15));
            Challenge actual = _repo.GetChallengeById(0);

            Assert.AreEqual(expected.ProposedDate2, actual.ProposedDate2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoChallengeTestsUpdateChallengeProposedDate3()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            Ladder ld1 = new Ladder { LadderID = 0 };

            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, ProposedDate3 = new DateTime(2016, 05, 05), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 }
            };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);

            Challenge expected = new Challenge { ChallengeID = 0, ProposedDate3 = new DateTime(2016, 05, 15), Ladder = ld1, Initiator = mtst1, Recipient = mtst2 };
            bool result = _repo.UpdateChallengeProposedDate3(0, new DateTime(2016, 05, 15));
            Challenge actual = _repo.GetChallengeById(0);

            Assert.AreEqual(expected.ProposedDate3, actual.ProposedDate3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoChallengeTestsUpdateChallengeAccepted()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            Ladder ld1 = new Ladder { LadderID = 0 };

            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Accepted = false, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 }
            };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);

            Challenge expected = new Challenge { ChallengeID = 0, Accepted = true, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 };
            bool result = _repo.UpdateChallengeAccepted(0, true);
            Challenge actual = _repo.GetChallengeById(0);

            Assert.AreEqual(expected.Accepted, actual.Accepted);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RepoChallengeTestsDeleteChallengeById()
        {
            MainTeam mt1 = new MainTeam { TeamID = 0, TeamName = "ABC" };
            MainTeam mt2 = new MainTeam { TeamID = 1, TeamName = "DEF" };
            MainTeam mt3 = new MainTeam { TeamID = 2, TeamName = "GHI" };
            SubTeam mtst1 = new SubTeam { SubTeamID = 0, MainTeam = mt1 };
            SubTeam mtst2 = new SubTeam { SubTeamID = 1, MainTeam = mt2 };
            SubTeam mtst3 = new SubTeam { SubTeamID = 2, MainTeam = mt3 };
            Ladder ld1 = new Ladder { LadderID = 0 };

            List<Challenge> cdb = new List<Challenge>
            {
                new Challenge { ChallengeID = 0, Ladder = ld1, Initiator = mtst1, Recipient = mtst2 },
                new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 }
            };

            _challengeSet.Object.AddRange(cdb);
            ConnectMocksToDataStore(cdb);
            _challengeSet.Setup(o => o.Remove(It.IsAny<Challenge>())).Callback((Challenge c) => cdb.Remove(c));

            Challenge expected = new Challenge { ChallengeID = 1, Ladder = ld1, Initiator = mtst1, Recipient = mtst3 };
            bool result = _repo.DeleteChallengeById(0);
            List<Challenge> actual = _repo.GetAllChallenges();

            Assert.AreEqual(expected, actual[0]);
            Assert.IsTrue(result);
        }


    }
}
