using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NashGaming.Models;

namespace NashGaming.Tests.Models
{
    [TestClass]
    public class TeamInviteTests
    {
        [TestMethod]
        public void TeamInviteTestsEnsureICanCreateAnInstance()
        {
            TeamInvite invite = new TeamInvite
            {
                Accepted = false,
                TeamInviteID = 0,
                DateSent = new DateTime().Date,
                InvitedGamer = new Gamer { GamerID = 0},
                Team = new MainTeam { TeamName = "Sudsy" }
            };

            Assert.IsNotNull(invite);
            Assert.AreEqual("Sudsy", invite.Team.TeamName);
        }
    }
}
