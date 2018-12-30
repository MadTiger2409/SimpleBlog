using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Commands.Account;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;
        private readonly IPasswordManager _passwordManager;

        public UserService(BlogContext context, IPasswordManager passwordManager)
        {
            _context = context;
            _passwordManager = passwordManager;
        }

        public async Task<User> GetUserAsync(int id)
            => await Task.FromResult(_context.Users.Include(x => x.Comments).SingleOrDefault(x => x.Id == id));

        public async Task<User> GetUserAsync(string login)
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.Login.ToLowerInvariant() == login.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetUsersAsync()
            => await Task.FromResult(_context.Users.AsEnumerable());

        public async Task RegisterUserAsync(CreateUserCommand command)
        {
            var user = await GetUserAsync(command.Login);
            if (user != null)
            {
                throw new InternalSystemException($"Login '{command.Login}' is not available. Try another one.");
            }

            byte[] passwordHash, passwordSalt;
            _passwordManager.CalculatePasswordHash(command.Password, out passwordHash, out passwordSalt);

            _context.Users.Add(new User(passwordHash, passwordSalt, command.Login));
            await _context.SaveChangesAsync();
        }
    }
}
