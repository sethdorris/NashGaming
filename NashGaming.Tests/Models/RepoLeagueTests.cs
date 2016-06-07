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

        [TestMethod]
        public void RepoLeagueGetLeaguesByGamesPerWeek()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2 },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo"},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD" },
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo" }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD" },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo"},
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo" }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesByGamesPerWeek(1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueGetLeaguesBySeasonLength()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2 },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4 },
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 5 }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4 },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4 }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesBySeasonLength(4);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsGetLeagueByStartDate()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2, StartDate = new DateTime(2016, 05, 02) },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, StartDate = new DateTime(2016, 05, 01)},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, StartDate = new DateTime(2016, 05, 01)},
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 5, StartDate = new DateTime(2016, 05, 02) }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, StartDate=new DateTime(2016, 05, 01) },
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, StartDate=new DateTime(2016, 05, 01) }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesByStartDate(new DateTime(2016, 05, 01));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsGetLeagueByEndDate()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2, EndDate = new DateTime(2016, 05, 02) },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, EndDate = new DateTime(2016, 05, 01)},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, EndDate = new DateTime(2016, 05, 01)},
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 5, EndDate = new DateTime(2016, 05, 02) }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, EndDate=new DateTime(2016, 05, 01) },
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, EndDate=new DateTime(2016, 05, 01) }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesByEndDate(new DateTime(2016, 05, 01));
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsGetLeagueByMinPlayer()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2, MinPlayers = 2 },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, MinPlayers = 4},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, MinPlayers = 4},
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 5, MinPlayers  =2 }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, MinPlayers =4  },
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, MinPlayers = 4 }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesByMinPlayers(4);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsGetLeagueByMaxPlayer()
        {
            List<League> LeagueDB = new List<League>
            {
                new League { LeagueID = 0, GamesPerWeek = 2, MaxPlayers = 2 },
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, MaxPlayers = 4},
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, MaxPlayers = 4},
                new League { LeagueID = 3, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 5, MaxPlayers  =2 }
            };
            List<League> expected = new List<League>
            {
                new League { LeagueID = 1, GamesPerWeek = 1, GameTitle = "Halo", SeasonLength = 4, MaxPlayers =4  },
                new League { LeagueID = 2, GamesPerWeek = 1, GameTitle = "COD", SeasonLength = 4, MaxPlayers = 4 }
            };
            _leagueSet.Object.AddRange(LeagueDB);
            ConnectMocksToDataStore(LeagueDB);
            List<League> actual = _repo.GetLeaguesByMaxPlayers(4);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsCreateALeague()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo" }
            };
            League added = new League { LeagueID = 1, GameTitle = "COD" };
            _leagueSet.Object.AddRange(l);
            _leagueSet.Setup(o => o.Add(It.IsAny<League>())).Callback((League league) => l.Add(league));
            ConnectMocksToDataStore(l);
            bool result = _repo.AddLeague(added);
            List<League> expected = new List<League>
            {
                new League { LeagueID = 0, GameTitle = "Halo" },
                new League { LeagueID = 1, GameTitle = "COD" }
            };
            List<League> actual = _repo.GetAllLeagues();
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);  
        }

        [TestMethod]
        public void RepoLeagueTestsGetLeagueByID()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo" }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            League expected = l[0];
            League actual = _repo.GetLeagueByID(0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLeagueTestsInactivateALeague()
        {
            List<League> ldb = new List<League>
            {
                new League { LeagueID = 0, GameTitle = "Halo", Active = true }
            };
            _leagueSet.Object.AddRange(ldb);
            ConnectMocksToDataStore(ldb);
            List<League> expected = new List<League>
            {
                new League { LeagueID = 0, GameTitle = "Halo", Active = false }
            };
            bool actual = _repo.InactivateLeague(0);
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void RepoLeagueTestsUpdateMinPlayers()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MinPlayers = 2 }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.UpdateLeagueMinPlayers(0, 4);
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MinPlayers = 4 }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLeagueTestsUpdateMaxPlayers()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MaxPlayers = 2 }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.UpdateLeagueMaxPlayers(0, 4);
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MaxPlayers = 4 }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLeagueTestsUpdateLeagueName()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MaxPlayers = 2, LeagueName = "Blah" }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.UpdateLeagueName(0, "hi");
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MaxPlayers = 2, LeagueName = "hi" }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLeagueTestsUpdateGamesPerWeek()
        {
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MaxPlayers = 2, GamesPerWeek = 1 }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.UpdateLeagueGamesPerWeek(0, 2);
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MaxPlayers = 2, GamesPerWeek = 2 }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoLeagueTestsRemovePosts()
        {
            Posts p = new Posts { PostsID = 0, Author = new Gamer(), Content = "Hi" };
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MaxPlayers = 2, Feed = new List<Posts> { p } }
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.RemovePostFromLeague(0, p);
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MaxPlayers = 2, Feed = new List<Posts>() }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RepoLeagueTestsAddPosts()
        {
            Posts p = new Posts { PostsID = 0, Author = new Gamer(), Content = "Hi" };
            List<League> l = new List<League>
            {
                new League {LeagueID = 0, GameTitle = "Halo", MaxPlayers = 2, Feed = new List<Posts>()}
            };
            _leagueSet.Object.AddRange(l);
            ConnectMocksToDataStore(l);
            bool result = _repo.AddPostToLeague(0, p);
            List<League> actual = _repo.GetAllLeagues();
            List<League> expected = new List<League>
            {
                new League {LeagueID =0, GameTitle = "Halo", MaxPlayers = 2, Feed = new List<Posts> { p } }
            };
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
