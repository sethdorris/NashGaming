using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class GamerTests
    {
        [TestMethod]
        public void GamerTestsEnsureAGamerHasAllFields()
        {
            Team new_team = new Team();
            Gamer new_gamer = new Gamer
            {
                GamerID = 1,
                Handle = "StiffNasty",
                MemberOf = new_team,
                Platform = "PS4",
                RealUser = null
            };
            Assert.AreEqual(1, new_gamer.GamerID);
            Assert.AreEqual("StiffNasty", new_gamer.Handle);
            Assert.AreEqual(new_team, new_gamer.MemberOf);
            Assert.AreEqual("PS4", new_gamer.Platform);
            Assert.AreEqual(null, new_gamer.RealUser);

        }
    }
}
