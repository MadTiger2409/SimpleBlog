using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Commands.Post
{
    public class UpdatePostCommand : CreatePostCommand
    {
        public int Id { get; set; }

        public UpdatePostCommand() { }

        public UpdatePostCommand(Data.Models.Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Description = post.Description;
            Content = post.Content;
            Tags = post.Tags;
        }
    }
}
