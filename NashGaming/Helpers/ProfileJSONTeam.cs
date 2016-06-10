using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Helpers
{
    public class ProfileJSONTeam
    {
        public string MainTeamName { get; set; }
        public string LogoLink { get; set; }
        public string Website { get; set; }
        public DateTime DateFounded { get; set; }
        public MainTeam MainTeam { get; set; }
        public ProfileJSONTeam()
        {
                
        }
        public ProfileJSONTeam(MainTeam mt)
        {
            MainTeam = mt;
        }
        public ProfileJSONTeam Serialize()
        {
            ProfileJSONTeam result = new ProfileJSONTeam();
            result.DateFounded = MainTeam.DateFounded;
            result.LogoLink = MainTeam.LogoLink;
            result.Website = MainTeam.Website;
            result.MainTeamName = MainTeam.TeamName;
            return result;
        }
    }
}
