using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Posts : IComparable 
    {
        public int PostsID { get; set; }
        public virtual Gamer Author  { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public virtual League League { get; set; }
        public virtual Ladder Ladder { get; set; }

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
            return a.PostsID == this.PostsID;
        }
    }
}