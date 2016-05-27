using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class LadderTests
    {
        [TestMethod]
        public void LadderTestsEnsureICanCreateAnInstance()
        {
            Ladder l = new Ladder { LadderID = 0, GameTitle = "Halo", MaxPlayers = 2 };
            Assert.IsNotNull(l);
            Assert.AreEqual(0, l.LadderID);
            Assert.AreEqual("Halo", l.GameTitle);
        }
    }
}
