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
}
