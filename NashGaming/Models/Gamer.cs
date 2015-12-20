using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Gamer
    {
        public int GamerID { get; set; }
        public string Handle { get; set; }
        public string Platform { get; set; }
        public Team MemberOf { get; set; }
        public string Game { get; set; }
    }
}