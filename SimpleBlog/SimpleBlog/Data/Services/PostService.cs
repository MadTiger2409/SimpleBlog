using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task EditPostAsync(UpdatePostCommand command)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.Id == command.Id);
            post.Update(command.Title, command.Description, command.Content, command.Tags);

            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(int id)
            => await _context.Posts.Include(x => x.Comments).ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Post>> GetPostsAsync()
            => await Task.FromResult(_context.Posts.AsEnumerable());

        public async Task<IEnumerable<Post>> GetPostsAsync(string tag)
            => await Task.FromResult(_context.Posts.Where(x => x.Tags.ToLowerInvariant().Contains(tag.ToLowerInvariant())));

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
        }
    }
}
