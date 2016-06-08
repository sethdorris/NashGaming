using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGaming.Models
{
	public class JsonReturnGamers
	{
		public int GamerID { get; set; }
		public virtual string RealUserID { get; set; }
		public string Email { get; set; }
		public string XB1Gamertag { get; set; }
		public string PSNID { get; set; }
		public virtual MainTeam MainTeam { get; set; }
		public virtual List<Posts> Comments { get; set; }
		public virtual List<TeamInvite> TeamInvites { get; set; }
		public string DisplayName { get; set; }
		public bool Active { get; set; }
		public List<Gamer> _GamerList { get; set; }
		public Gamer SingleGamer { get; set; }

		public JsonReturnGamers()
		{

		}

		public JsonReturnGamers(List<Gamer> gamers)
		{
			_GamerList = gamers;
		}

		public override bool Equals(object obj)
		{
			Gamer a = obj as Gamer;
			if (obj == null)
			{
				return false;
			}
			return a.GamerID == this.GamerID;
		}


		public List<JsonReturnGamers> ReturnGamersAsJson()
		{
			List<JsonReturnGamers> result = new List<JsonReturnGamers>();
			for (int i = 0; i < _GamerList.Count; i++)
			{
				JsonReturnGamers row = new JsonReturnGamers();
				row.Active = _GamerList[i].Active;
				row.Comments = _GamerList[i].Comments;
				row.DisplayName = _GamerList[i].DisplayName;
				row.Email = _GamerList[i].Email;
				row.GamerID = _GamerList[i].GamerID;
				row.MainTeam = _GamerList[i].MainTeam;
				row.PSNID = _GamerList[i].PSNID;
				row.RealUserID = _GamerList[i].RealUserID;
				row.XB1Gamertag = _GamerList[i].XB1Gamertag;
				result.Add(row);
			}
			return result;
		}

		public JsonReturnGamers ReturnGamerAsJson()
		{
			JsonReturnGamers single = new JsonReturnGamers();
			single.Active = SingleGamer.Active;
			single.Comments = SingleGamer.Comments;
			single.DisplayName = SingleGamer.DisplayName;
			single.Email = SingleGamer.Email;
			single.GamerID = SingleGamer.GamerID;
			single.MainTeam = SingleGamer.MainTeam;
			single.PSNID = SingleGamer.PSNID;
			single.RealUserID = SingleGamer.RealUserID;
			single.XB1Gamertag = SingleGamer.XB1Gamertag;
			return single;

		}
	}
}