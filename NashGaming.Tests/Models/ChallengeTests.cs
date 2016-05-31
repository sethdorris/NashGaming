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
    public class ChallengeTests
    {
        [TestMethod]
        public void ChallengeTestsEnsureICanCreateAChallenge()
        {
            Challenge c = new Challenge { ChallengeID = 0, Accepted = true };
            Assert.IsNotNull(c);
            Assert.AreEqual(0, c.ChallengeID);
            Assert.AreEqual(true, c.Accepted);
        }
    }
}
