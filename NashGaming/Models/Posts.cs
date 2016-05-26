using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Posts : IComparable 
    {
        [Key]
        public int PostID { get; set; }
        public virtual Gamer Author  { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public virtual int LeagueID { get; set; }

        public int CompareTo(object obj)
        {
            Posts Other_Post = obj as Posts;
            return -1 * (this.Date.CompareTo(Other_Post.Date));
        }
        public override bool Equals(object obj)
        {
            Posts a = obj as Posts;
            if (obj == null)
            {
                return false;
            }
            return a.PostID == this.PostID;
        }
    }
}