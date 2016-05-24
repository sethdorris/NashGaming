using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class LeagueModelTests
    {
        [TestMethod]
        public void LeagueModelTestsEnsureICanCreateInstanceOfLeague()
        {
            League a = new League();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void LeagueModelTestsEnsureAllPropsAreWorking()
        {
            League a = new League { Platform = "PS4", GameTitle = "RS6", LeagueID = 1, LeagueName = "2v2 TDM" };
            Assert.AreEqual("PS4", a.Platform);
            Assert.AreEqual("RS6", a.GameTitle);
            Assert.AreEqual(1, a.LeagueID);
            Assert.AreEqual("2v2 TDM", a.LeagueName);
        }
    }
}
