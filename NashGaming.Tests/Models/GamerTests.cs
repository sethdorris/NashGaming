using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System.Collections.Generic;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class GamerTests
    {
        [TestMethod]
        public void GamerTestsEnsureAGamerIsNotNull()
        {
            Gamer new_gamer = new Gamer();
            Assert.IsNotNull(new_gamer);
        }

        [TestMethod]
        public void GamerTestsEnsurePropAssignmentsWork()
        {
            Gamer actual = new Gamer { Username = "Seth", GamerID = 0, PSNID = "Stiff" };
            Assert.AreEqual("Seth", actual.Username);
            Assert.AreEqual(0, actual.GamerID);
            Assert.AreEqual("Stiff", actual.PSNID);
        }
    }
}
