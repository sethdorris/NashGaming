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

        public int CompareTo(object obj)
        {
            Posts Other_Post = obj as Posts;
            return -1 * (this.Date.CompareTo(Other_Post.Date));
        }
    }
}