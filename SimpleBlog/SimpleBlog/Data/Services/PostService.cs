using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Post> GetPostAsync(int id)
            => await Task.FromResult(_context.Posts.SingleOrDefault(x => x.Id == id));

        public async Task<IEnumerable<Post>> GetPostsAsync()
            => await Task.FromResult(_context.Posts.AsEnumerable());
    }
}
