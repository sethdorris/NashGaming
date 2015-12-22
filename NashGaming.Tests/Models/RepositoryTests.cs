using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System.Data.Entity;
using Moq;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void RepositoryTestsEnsureICanCreateAnInstance()
        {
            NashGamingRepository Repo = new NashGamingRepository();
            Assert.IsNotNull(Repo);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanCreateAnInstancePassingAContext()
        {
            NashGamingContext myContext = new NashGamingContext();
            NashGamingRepository Repo = new NashGamingRepository(myContext);
            Assert.AreEqual(myContext, Repo.Context);
        }

        [TestMethod]
        public void RepositoryTestsEnsureICanGetAllUsers()
        {
            NashGamingRepository Repo = new NashGamingRepository();
            var mock_context = new Mock<NashGamingContext>();
            var mock_users = new Mock<DbSet<Gamer>>();
            
        }
    }
}
