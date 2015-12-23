using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Gamer : IComparable
    {
        [Key]
        public int GamerID { get; set; }
        public virtual ApplicationUser RealUser { get; set; }
        public string Handle { get; set; }
        public string Platform { get; set; }
        public virtual Team MemberOf { get; set; }
        public List<Posts> Comments { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}