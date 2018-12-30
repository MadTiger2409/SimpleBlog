using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Comment;
using SimpleBlog.Data.Models;

namespace SimpleBlog.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; }
        public CreateCommentCommand NewComment { get; set; }

        public PostDetailsViewModel() { }

        public PostDetailsViewModel(Post post)
        {
            Post = post;
        }
    }
}
