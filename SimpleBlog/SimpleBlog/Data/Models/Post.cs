using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }

        public Admin Admin { get; set; }
        public List<Comment> Comments { get; set; }

        public Post(string title, string description, string content, string tags) : base()
        {
            Title = title;
            Description = description;
            Content = content;
            Tags = tags;
        }
    }
}
