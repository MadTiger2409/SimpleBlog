using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Post;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;

        public PostService(BlogContext context)
        {
            _context = context;
        }

        public async Task AddPostAsync(CreatePostCommand command, Admin admin)
        {
            var post = new Post(command.Title, command.Description, command.Content, command.Tags);

            admin.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(int id)
            => await Task.FromResult(_context.Posts.SingleOrDefault(x => x.Id == id));

        public async Task<IEnumerable<Post>> GetPostsAsync()
            => await Task.FromResult(_context.Posts.AsEnumerable());
    }
}
