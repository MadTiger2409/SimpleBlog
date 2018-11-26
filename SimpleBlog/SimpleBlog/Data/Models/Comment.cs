using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Comment : Entity
    {
        public string Content { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }

        public Comment() : base()
        {

        }
    }
}
