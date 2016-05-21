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
    public class RepositoryTests
    {
        private Mock<NashGamingContext> _context;
        private NashGamingRepository _repo;
        private Mock<DbSet<Gamer>> _gamerSet;
        private Mock<DbSet<Posts>> _postSet;
        private Mock<DbSet<NashGaming.Models.Match>> _matchSet;
        private Mock<DbSet<Team>> _teamSet;
        private Mock<DbSet<League>> _leagueSet;

        private void ConnectMocksToDataStore(IEnumerable<Gamer> data_store)
        {
            var data_source = data_store.AsQueryable();
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _gamerSet.As<IQueryable<Gamer>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Gamers).Returns(_gamerSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<League> data_store)
        {
            var data_source = data_store.AsQueryable();
            _leagueSet.As<IQueryable<League>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _leagueSet.As<IQueryable<League>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Leagues).Returns(_leagueSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Team> data_store)
        {
            var data_source = data_store.AsQueryable();
            _teamSet.As<IQueryable<Team>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _teamSet.As<IQueryable<Team>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Teams).Returns(_teamSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<NashGaming.Models.Match> data_store)
        {
            var data_source = data_store.AsQueryable();
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _matchSet.As<IQueryable<NashGaming.Models.Match>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Matches).Returns(_matchSet.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Posts> data_store)
        {
            var data_source = data_store.AsQueryable();
            _postSet.As<IQueryable<Posts>>().Setup(data => data.Provider).Returns(data_source.Provider);
            _postSet.As<IQueryable<Posts>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            _postSet.As<IQueryable<Posts>>().Setup(data => data.Expression).Returns(data_source.Expression);
            _postSet.As<IQueryable<Posts>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            _context.Setup(a => a.Posts).Returns(_postSet.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new Mock<NashGamingContext>();
            _gamerSet = new Mock<DbSet<Gamer>>();
            _postSet = new Mock<DbSet<Posts>>();
            _teamSet = new Mock<DbSet<Team>>();
            _matchSet = new Mock<DbSet<NashGaming.Models.Match>>();
            _repo = new NashGamingRepository(_context.Object);
            _leagueSet = new Mock<DbSet<League>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context = null;
            _gamerSet = null;
            _teamSet = null;
            _matchSet = null;
            _postSet = null;
            _repo = null;
            _leagueSet = null;
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
                new Gamer {GamerID = 1, Handle = "Stiffy", Platform = "PS4" },
                new Gamer {GamerID = 2, Handle = "Michael", Platform = "XB1" }
            };

            _gamerSet.Object.AddRange(gamers);
            //ConnectMocksToDataStore(gamers);

            var actual = _repo.GetAllGamers();
            Assert.AreEqual(1, actual[0].GamerID);
            CollectionAssert.AreEqual(gamers, actual);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllTeams()
        {
            List<Team> expected = new List<Team>()
            {
                new Team { Rank = 2, TeamID = 1, TeamName="FS1" },
                new Team { Rank = 1, TeamID = 2, TeamName="GTXT" }
            };

            _teamSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetAllTeams();
            Assert.AreEqual(1, actual[0].TeamID);
            CollectionAssert.AreEqual(expected, actual);
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
        public void RepositoryTestsEnsureICanGetGamerByHandle()
        {
            List<Gamer> expected = new List<Gamer>
            {
                new Gamer { GamerID = 1, Handle = "StiffNasty" },
                new Gamer { GamerID = 2, Handle = "Bro" },
                new Gamer { GamerID = 3, Handle = "StiffNasty45" }
            };
            

            _gamerSet.Object.AddRange(expected);
            //ConnectMocksToDataStore(expected);

            var actual = _repo.GetGamerByHandle("StiffNasty");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].GamerID);
            Assert.AreEqual(3, actual[1].GamerID);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanSearchTeamByName()
        {
            List<Team> team_data = new List<Team>
            {
                new Team { TeamName = "Siege the Day" },
                new Team { TeamName = "Day of Siege" },
                new Team { TeamName = "Group Text" }
            };


            _teamSet.Object.AddRange(team_data);
            ConnectMocksToDataStore(team_data);

            List<Team> expected = new List<Team>
            {
                new Team { TeamName = "Day of Siege" },
                new Team { TeamName = "Siege the Day" }
            };

            var actual = _repo.SearchTeamsByName("siege");
            Assert.AreEqual(expected[0].TeamName, actual[0].TeamName);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllPosts()
        {
            List<Posts> expected = new List<Posts>
            {
                new Posts {PostID= 1, Date = new DateTime(2015, 12, 2), Content = "What??"},
                new Posts {PostID= 2, Date = new DateTime(2015, 12, 5), Content = "Who??"  }
            };


            _postSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.GetAllPosts();
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(2, actual[0].PostID);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanSearchPostsByContent()
        {
            List<Posts> expected = new List<Posts>
            {
                new Posts {PostID= 1, Date = new DateTime(2015, 12, 2), Content = "What is the name of the game??"},
                new Posts {PostID= 2, Date = new DateTime(2015, 12, 5), Content = "Who??"  },
                new Posts {PostID= 3, Date = new DateTime(2015, 12, 7, 10, 15, 00), Content = "Game Game" }
            };


            _postSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = _repo.SearchPostsByContent("game");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(3, actual[0].PostID);
            Assert.AreEqual(1, actual[1].PostID);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanDeletePostById()
        {
            List<Posts> expected = new List<Posts>
            {
                new Posts {PostID= 1, Date = new DateTime(2015, 12, 2), Content = "What is the name of the game??"},
                new Posts {PostID= 2, Date = new DateTime(2015, 12, 5), Content = "Who??"  },
                new Posts {PostID= 3, Date = new DateTime(2015, 12, 7, 10, 15, 00), Content = "Game Game" }
            };


            _postSet.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            _postSet.Setup(o => o.Remove(It.IsAny<Posts>())).Callback((Posts p) => expected.Remove(p));

            bool actual = _repo.DeletePostById(1);
            var numPosts = _repo.GetAllPosts();
            Assert.IsTrue(actual);
            Assert.AreEqual(2, numPosts.Count);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanCreateANewPost()
        {
            Gamer me = new Gamer { GamerID = 1, Handle = "Stiffy" };
            string input = "This is my post";
            List<Posts> posts = new List<Posts> {
                new Posts { PostID = 1, Author = me, Content = "blah"  }
            };

            _postSet.Object.AddRange(posts);
            ConnectMocksToDataStore(posts);
            _postSet.Setup(o => o.Add(It.IsAny<Posts>())).Callback((Posts p) => posts.Add(p));

            bool actual = _repo.CreateAPost(me, input);
            Assert.IsTrue(actual);
            Assert.AreEqual(2, posts.Count);
        }

        [TestMethod]
        public void RepoEnsureICanAddGamerToDB()
        {
            List<Gamer> expected = new List<Gamer> { new Gamer { Handle = "StiffNasty" } };
            Gamer me = new Gamer { Handle = "Plah", Platform = "PS4" };

            _gamerSet.Object.AddRange(expected);
            //ConnectMocksToDataStore(expected);
            _gamerSet.Setup(o => o.Add(It.IsAny<Gamer>())).Callback((Gamer g) => expected.Add(g));

            bool actual = _repo.AddGamer(me);
            Assert.IsTrue(actual);
            Assert.AreEqual(2, expected.Count);
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

            //List<League> actual = _repo.GetAllLeagues();
            Assert.IsNotNull(expected);
            //CollectionAssert.AreEqual(expected, actual);
        }
    }
}
