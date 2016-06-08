using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
    public class JsonReturnInvites
    {
        public int TeamInviteID { get; set; }
        public virtual MainTeam Team { get; set; }
        public virtual Gamer InvitedGamer { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateAccepted { get; set; }
        public bool Accepted { get; set; }
        public List<TeamInvite> _InviteList { get; set; }
        public TeamInvite _Invite { get; set; }
        public override bool Equals(object obj)
        {
            TeamInvite i = obj as TeamInvite;
            if (obj == null)
            {
                return false;
            }
            return i.TeamInviteID == this.TeamInviteID;
        }
        public JsonReturnInvites()
        {

        }
        public JsonReturnInvites(List<TeamInvite> invitelist)
        {
            _InviteList = invitelist;
        }

        public JsonReturnInvites(TeamInvite inv)
        {
            _Invite = inv;
        }

        public List<JsonReturnInvites> ReturnTeamInvitesAsJson()
        {
            List<JsonReturnInvites> result = new List<JsonReturnInvites>();
            for(int i = 0; i < _InviteList.Count; i++)
            {
                JsonReturnInvites row = new JsonReturnInvites();
                row.Accepted = _InviteList[i].Accepted;
                row.DateAccepted = _InviteList[i].DateAccepted;
                row.DateSent = _InviteList[i].DateSent;
                row.InvitedGamer = _InviteList[i].InvitedGamer;
                row.Team = _InviteList[i].Team;
                row.TeamInviteID = _InviteList[i].TeamInviteID;
                result.Add(row);
            }
            return result;
        }
        public JsonReturnInvites ReturnTeamInviteAsJson()
        {
            JsonReturnInvites result = new JsonReturnInvites();
            result.Accepted = _Invite.Accepted;
            result.DateAccepted = _Invite.DateAccepted;
            result.DateSent = _Invite.DateSent;
            result.InvitedGamer = _Invite.InvitedGamer;
            result.Team = _Invite.Team;
            result.TeamInviteID = _Invite.TeamInviteID;
            return result;
        }
    }
}
