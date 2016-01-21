using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NashGaming.Models
{
    public class Gamer : IdentityUser, IComparable
    {
        [Key]
        public int GamerID { get; set; }
        public virtual string RealUserID { get; set; }
        public string Handle { get; set; }
        public string Platform { get; set; }
        public virtual Team MemberOf { get; set; }
        public List<Posts> Comments { get; set; }

        public int CompareTo(object obj)
        {
            Gamer gamer = obj as Gamer;
            return this.Handle.CompareTo(gamer.Handle);
        }
    }
}