using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Post;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<IEnumerable<Post>> GetPostsAsync(string tag);
        Task<Post> GetPostAsync(int id);
        Task AddPostAsync(CreatePostCommand command, Admin admin);
    }
}
