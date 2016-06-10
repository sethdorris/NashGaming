using NashGaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Helpers
{
    public class ProfileJsonHelper
    {
        public ProfileJSONTeam TeamInfo { get; set; }
        public List<JsonReturnInvites> Invites { get; set; }
        public List<JsonConverterLadder> Ladders { get; set; }
        public List<JsonConverterLeague> Leagues { get; set; }
        public List<JsonConverterMatches> Matches { get; set; }
    }
}
