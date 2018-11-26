using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(int id);
    }
}
