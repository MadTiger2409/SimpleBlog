using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Post : Entity
    {
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
        public string Tags { get; set; }

        public User User { get; set; }

        public Post() : base()
        {

        }
    }
}
