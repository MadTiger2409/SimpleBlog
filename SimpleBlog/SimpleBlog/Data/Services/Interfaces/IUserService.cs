using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.User;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string login);
        Task<IEnumerable<User>> GetUsersAsync();
        Task RegisterUserAsync(CreateUserCommand command);
    }
}
