using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }
        public virtual Gamer Author  { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}