using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Comment;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        public async Task CreateCommentAsync(CreateCommentCommand command, User user, Post post)
        {
            var comment = new Comment { Content = command.Content };
            user.Comments.Add(comment);
            post.Comments.Add(comment);

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
