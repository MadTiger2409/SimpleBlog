using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(int id)
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetUserAsync(string login)
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.Login.ToLowerInvariant() == login.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetUsersAsync()
            => await Task.FromResult(_context.Users.AsEnumerable());
    }
}
