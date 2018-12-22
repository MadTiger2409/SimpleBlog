using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Account;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> LoginAccountAsync(LogInCommand command);
    }
}
