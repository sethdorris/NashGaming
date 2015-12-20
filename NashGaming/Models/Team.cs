using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public List<Gamer> Roster { get; set; }
        public DateTime DateFounded { get; set; }
        public virtual Gamer Founder { get; set; }
        public int Rank { get; set; }
        public string Website { get; set; }

    }
}